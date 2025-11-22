using ClinicManagement.Data;
using ClinicManagement.Models.Auth;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace ClinicManagement.Services.Auth
{
    public interface IUserService : IService<User>
    {
        Task<IEnumerable<Role>> GetAllRolesAsync(CancellationToken token = default);
        Task UpdateUserRoleAsync(int userId, int roleId, CancellationToken token = default);
        Task UpdateUserPasswordAsync(int userId, string newPassword, CancellationToken token = default);
    }

    public class UserService : Service<User>, IUserService
    {
        private readonly DbSet<Role> _roles;

        public UserService(ClinicDbContext context, ILogger<UserService> logger)
            : base(context, logger)
        {
            _roles = _context.Set<Role>();
        }

        public override async Task<PaginatedResult<User>> GetAllAsync(
            int pageNumber = 1,
            int pageSize = 10,
            string? searchTerm = null,
            string? sortBy = null,
            bool sortAscending = true,
            CancellationToken token = default)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1 || pageSize > 100) pageSize = 10;

            try
            {
                var query = _dbSet.Include(u => u.Role).AsNoTracking();

                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    query = query.Where(u =>
                        u.FirstName.ToLower().Contains(searchTerm.ToLower()) ||
                        (u.MiddleName != null && u.MiddleName.ToLower().Contains(searchTerm.ToLower())) ||
                        (u.LastName != null && u.LastName.ToLower().Contains(searchTerm.ToLower())) ||
                        u.Email.ToLower().Contains(searchTerm.ToLower()) ||
                        (u.Role != null && u.Role.Name.ToLower().Contains(searchTerm.ToLower()))
                    );
                }

                if (!string.IsNullOrWhiteSpace(sortBy))
                {
                    query = ApplySorting(query, sortBy, sortAscending);
                }
                else
                {
                    query = ApplySorting(query, "Id", true);
                }

                var items = await query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync(token);

                var totalCount = await query.CountAsync(token);

                return new PaginatedResult<User>
                {
                    Items = items,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalCount = totalCount,
                    SearchTerm = searchTerm,
                    SortBy = sortBy,
                    SortAscending = sortAscending
                };
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning("GetAllAsync operation for User was canceled");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting User list");
                throw;
            }
        }

        public override async Task<User?> GetByIdAsync(int id, CancellationToken token = default)
        {
            try
            {
                return await _dbSet
                    .Include(u => u.Role)
                    .FirstOrDefaultAsync(u => u.Id == id, token);
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning("GetByIdAsync for User with id {Id} was canceled", id);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving User with id {Id}", id);
                throw;
            }
        }

        public async Task<IEnumerable<Role>> GetAllRolesAsync(CancellationToken token = default)
        {
            try
            {
                return await _roles
                    .AsNoTracking()
                    .OrderBy(r => r.Id)
                    .ToListAsync(token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving roles");
                throw;
            }
        }

        public async Task UpdateUserRoleAsync(int userId, int roleId, CancellationToken token = default)
        {
            try
            {
                var user = await _dbSet.FindAsync([userId], token);
                if (user == null)
                    throw new KeyNotFoundException($"User with id {userId} not found");

                var role = await _roles.FindAsync([roleId], token);
                if (role == null)
                    throw new KeyNotFoundException($"Role with id {roleId} not found");

                user.RoleId = roleId;
                await _context.SaveChangesAsync(token);

                _logger.LogInformation("Updated role for User with id {UserId} to Role {RoleId}", userId, roleId);
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning("UpdateUserRoleAsync for User with id {UserId} was canceled", userId);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating role for User with id {UserId}", userId);
                throw;
            }
        }

        public async Task UpdateUserPasswordAsync(int userId, string newPassword, CancellationToken token = default)
        {
            try
            {
                var user = await _dbSet.FindAsync([userId], token);
                if (user == null)
                    throw new KeyNotFoundException($"User with id {userId} not found");

                user.PasswordHash = HashPassword(newPassword);
                await _context.SaveChangesAsync(token);

                _logger.LogInformation("Updated password for User with id {UserId}", userId);
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning("UpdateUserPasswordAsync for User with id {UserId} was canceled", userId);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating password for User with id {UserId}", userId);
                throw;
            }
        }

        public override async Task AddAsync(User entity, CancellationToken token = default)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            if (await _context.Users.AnyAsync(u => u.Email == entity.Email, token))
                throw new ArgumentException("Email already exists.");

            if (string.IsNullOrWhiteSpace(entity.PasswordHash))
                throw new ArgumentException("Password is required.");

            // Hash the password if it's not already hashed
            if (!entity.PasswordHash.Contains("=") || entity.PasswordHash.Length < 40)
            {
                entity.PasswordHash = HashPassword(entity.PasswordHash);
            }

            entity.CreatedAt = DateTimeOffset.UtcNow;

            await base.AddAsync(entity, token);
        }

        private string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }
}

