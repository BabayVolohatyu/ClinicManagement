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
                    query = ApplySearchFilter(query, searchTerm);
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
                _logger.LogWarning("GetAllAsync operation for {Entity} was canceled", typeof(User).Name);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting {Entity} list", typeof(User).Name);
                throw;
            }
        }

        public override async Task<User?> GetByIdAsync(int id, CancellationToken token = default)
        {
            try
            {
                return await _dbSet
                    .Include(u => u.Role)
                    .Include(u => u.PromotionRequests)
                    .FirstOrDefaultAsync(u => u.Id == id, token);
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning("GetByIdAsync for {Entity} with id {Id} was canceled", typeof(User).Name, id);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving {Entity} with id {Id}", typeof(User).Name, id);
                throw;
            }
        }

        protected override IQueryable<User> ApplySearchFilter(IQueryable<User> query, string searchTerm)
        {
            return query.Where(u =>
                u.FirstName.Contains(searchTerm) ||
                (u.MiddleName != null && u.MiddleName.Contains(searchTerm)) ||
                (u.LastName != null && u.LastName.Contains(searchTerm)) ||
                u.Email.Contains(searchTerm) ||
                (u.Role != null && u.Role.Name.Contains(searchTerm))
            );
        }

        protected override IQueryable<User> ApplySorting(IQueryable<User> query, string sortBy, bool ascending)
        {
            if (sortBy == "Role.Name" || sortBy == "RoleId")
            {
                query = ascending ? query.OrderBy(u => u.Role != null ? u.Role.Name : "")
                    : query.OrderByDescending(u => u.Role != null ? u.Role.Name : "");
                return query;
            }
            return base.ApplySorting(query, sortBy, ascending);
        }

        public override async Task AddAsync(User entity, CancellationToken token = default)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                
                if (!string.IsNullOrWhiteSpace(entity.PasswordHash))
                {
                    entity.PasswordHash = HashPassword(entity.PasswordHash);
                }

                entity.CreatedAt = DateTimeOffset.UtcNow;

                
                entity.Role = null!;
                entity.PromotionRequests = null!;

                await base.AddAsync(entity, token);
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning("AddAsync for {Entity} was canceled", typeof(User).Name);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding {Entity}", typeof(User).Name);
                throw;
            }
        }

        public override async Task UpdateAsync(int id, User entity, CancellationToken token = default)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                var existingEntity = await GetByIdAsync(id, token);
                if (existingEntity == null)
                    throw new KeyNotFoundException($"{typeof(User).Name} with id {id} not found");

                
                entity.CreatedAt = existingEntity.CreatedAt;

                
                
                if (string.IsNullOrWhiteSpace(entity.PasswordHash))
                {
                    entity.PasswordHash = existingEntity.PasswordHash;
                }
                else
                {
                    entity.PasswordHash = HashPassword(entity.PasswordHash);
                }

                
                entity.Role = null!;
                entity.PromotionRequests = null!;

                _context.Entry(existingEntity).CurrentValues.SetValues(entity);
                _context.Entry(existingEntity).State = EntityState.Modified;

                await _context.SaveChangesAsync(token);

                _logger.LogInformation("Updated {Entity} with id {Id}", typeof(User).Name, id);
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning("UpdateAsync for {Entity} with id {Id} was canceled", typeof(User).Name, id);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating {Entity} with id {Id}", typeof(User).Name, id);
                throw;
            }
        }

        public async Task<IEnumerable<Role>> GetAllRolesAsync(CancellationToken token = default)
        {
            try
            {
                return await _roles
                    .OrderBy(r => r.Name)
                    .ToListAsync(token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting Role list");
                throw;
            }
        }

        private string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }
}

