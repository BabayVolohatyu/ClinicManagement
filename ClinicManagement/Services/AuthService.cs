using ClinicManagement.Data;
using ClinicManagement.Models.Auth;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text.Json;

namespace ClinicManagement.Services
{
    public class AuthService : IAuthService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ClinicDbContext _context;
        private readonly ILogger<AuthService> _logger;

        private const string SessionKey = "UserAuth";
        private const string GuestKey = "IsGuest";

        public AuthService(IHttpContextAccessor httpContextAccessor, ClinicDbContext context, ILogger<AuthService> logger)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        // ---------- Public API (під твої інтерфейсні сигнатури) ----------
        public async Task<bool> LoginAsync(string email, string password)
        {
            try
            {
                var user = await GetUserByEmailAsync(email);
                if (user == null || !VerifyPassword(password, user.PasswordHash))
                {
                    _logger.LogWarning("Failed login attempt for email: {Email}", email);
                    return false;
                }

                // підвантажити роль (якщо ще не підвантажено)
                if (user.Role == null)
                {
                    user = await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Id == user.Id);
                }

                LoginUser(user);
                _logger.LogInformation("User {Email} logged in successfully", email);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during login for email: {Email}", email);
                throw;
            }
        }

        public async Task<bool> RegisterAsync(RegisterModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            try
            {
                if (await UserExistsAsync(model.Email))
                {
                    _logger.LogWarning("Registration failed - user already exists: {Email}", model.Email);
                    return false;
                }

                var authorizedRole = await GetRoleByNameAsync("Authorized");
                if (authorizedRole == null)
                {
                    _logger.LogError("Authorized role not found during registration");
                    return false;
                }

                var user = new User
                {
                    FirstName = model.FirstName,
                    MiddleName = model.MiddleName,
                    LastName = model.LastName,
                    Email = model.Email,
                    PasswordHash = HashPassword(model.Password),
                    RoleId = authorizedRole.Id,
                    CreatedAt = DateTimeOffset.UtcNow,
                    IsActive = true
                };

                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();

                // підвантажити роль для сесії
                user = await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Id == user.Id);

                LoginUser(user);
                _logger.LogInformation("User registered successfully: {Email}", model.Email);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during registration for email: {Email}", model.Email);
                throw;
            }
        }

        public void LoginUser(User user)
        {
            try
            {
                if (user == null) throw new ArgumentNullException(nameof(user));

                var sessionUser = new SessionUser
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    RoleId = user.RoleId,
                    RoleName = user.Role?.Name
                };

                var userData = JsonSerializer.Serialize(sessionUser);
                var http = _httpContextAccessor.HttpContext;
                http?.Session.Clear(); // уникнути залишків попередніх сесій
                http?.Session.SetString(SessionKey, userData);
                http?.Session.Remove(GuestKey);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during user login session setup");
                throw;
            }
        }

        public void Logout()
        {
            try
            {
                var http = _httpContextAccessor.HttpContext;
                http?.Session.Remove(SessionKey);
                http?.Session.Remove(GuestKey);
                _logger.LogInformation("User logged out");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during logout");
                throw;
            }
        }

        public SessionUser? GetCurrentUser()
        {
            try
            {
                var userData = _httpContextAccessor.HttpContext?.Session.GetString(SessionKey);
                if (string.IsNullOrEmpty(userData)) return null;

                return JsonSerializer.Deserialize<SessionUser>(userData);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving current user from session");
                return null;
            }
        }

        public bool IsAuthenticated()
        {
            return GetCurrentUser() != null;
        }

        public bool IsGuest()
        {
            return _httpContextAccessor.HttpContext?.Session.GetString(GuestKey) == "true";
        }

        public void SetGuest()
        {
            try
            {
                var http = _httpContextAccessor.HttpContext;
                http?.Session.Clear();
                http?.Session.SetString(GuestKey, "true");
                http?.Session.Remove(SessionKey);
                _logger.LogInformation("Guest access granted");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error setting guest session");
                throw;
            }
        }

        // ---------- Data helpers ----------
        public async Task<User?> GetUserByEmailAsync(string email)
        {
            try
            {
                return await _context.Users
                    .AsNoTracking()
                    .Include(u => u.Role)
                    .FirstOrDefaultAsync(u => u.Email == email && u.IsActive);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving user by email: {Email}", email);
                throw;
            }
        }

        public async Task<bool> UserExistsAsync(string email)
        {
            try
            {
                return await _context.Users
                    .AsNoTracking()
                    .AnyAsync(u => u.Email == email);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking if user exists: {Email}", email);
                throw;
            }
        }

        public async Task<Role?> GetRoleByNameAsync(string roleName)
        {
            try
            {
                return await _context.Roles
                    .AsNoTracking()
                    .FirstOrDefaultAsync(r => r.Name == roleName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving role by name: {RoleName}", roleName);
                throw;
            }
        }

        // ---------- Password hashing ----------
        private string HashPassword(string password)
        {
            // використаємо 128-bit salt, як у твоєму початковому прикладі
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));

            return $"{Convert.ToBase64String(salt)}.{hashed}";
        }

        private bool VerifyPassword(string password, string storedHash)
        {
            try
            {
                var parts = storedHash.Split('.', 2);
                if (parts.Length != 2) return false;

                var salt = Convert.FromBase64String(parts[0]);
                var hashedPassword = parts[1];

                string verifiedHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: password,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 100000,
                    numBytesRequested: 256 / 8));

                return hashedPassword == verifiedHash;
            }
            catch
            {
                return false;
            }
        }
    }
}
