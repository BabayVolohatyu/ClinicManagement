using ClinicManagement.Data;
using ClinicManagement.Models.Auth;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Cryptography;
using System.Text;

namespace ClinicManagement.Services.Auth
{
    public interface IAuthService
    {
        Task<User?> LoginAsync(LoginModel model);
        Task<User> RegisterAsync(RegisterModel model);
        Task<User> GuestLoginAsync();
    }

    public class AuthService : IAuthService
    {
        private readonly ClinicDbContext _context;

        public AuthService(ClinicDbContext context)
        {
            _context = context;
        }

        public async Task<User?> LoginAsync(LoginModel model)
        {
            var user = await _context.Users.Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == model.Email);

            if (user == null)
                return null;

            if (!VerifyPassword(model.Password, user.PasswordHash))
                return null;

            return user;
        }

        public async Task<User> RegisterAsync(RegisterModel model)
        {
            if (model.Password != model.ConfirmPassword)
                throw new ArgumentException("Passwords do not match.");

            if (await _context.Users.AnyAsync(u => u.Email == model.Email))
                throw new ArgumentException("Email already used.");

            var user = new User
            {
                FirstName = model.FirstName,
                MiddleName = model.MiddleName,
                LastName = model.LastName,
                Email = model.Email,
                CreatedAt = DateTimeOffset.UtcNow,
                PasswordHash = HashPassword(model.Password),
                RoleId = (int)RoleType.Authorized
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            user.Role = await _context.Roles.FindAsync(user.RoleId);

            return user;
        }

        public async Task<User> GuestLoginAsync()
        {
            var guestRole = await _context.Roles.FirstOrDefaultAsync(r => r.Id == (int)RoleType.Guest);

            if (guestRole == null)
                throw new Exception("Guest role missing.");

            var user = new User
            {
                FirstName = "Guest",
                Email = $"guest_{Guid.NewGuid():N}@guest.local",
                CreatedAt = DateTimeOffset.UtcNow,
                PasswordHash = "",
                RoleId = guestRole.Id
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }

        private bool VerifyPassword(string plain, string hash)
        {
            return HashPassword(plain) == hash;
        }
    }
}
