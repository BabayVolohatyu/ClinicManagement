using ClinicManagement.Data;
using ClinicManagement.Models.Auth;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ClinicManagement.Services.Auth
{
    public interface IPromotionRequestService : IService<PromotionRequest>
    {
        Task<IEnumerable<User>> GetAllUsersAsync(CancellationToken token = default);
        Task<IEnumerable<Role>> GetAllRolesAsync(CancellationToken token = default);
        Task<User?> GetCurrentUserAsync(CancellationToken token = default);
        bool IsCurrentUserAdmin();
    }

    public class PromotionRequestService : Service<PromotionRequest>, IPromotionRequestService
    {
        private readonly DbSet<User> _users;
        private readonly DbSet<Role> _roles;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PromotionRequestService(ClinicDbContext context, ILogger<PromotionRequestService> logger, IHttpContextAccessor httpContextAccessor)
            : base(context, logger)
        {
            _users = _context.Set<User>();
            _roles = _context.Set<Role>();
            _httpContextAccessor = httpContextAccessor;
        }

        public override async Task<PaginatedResult<PromotionRequest>> GetAllAsync(
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
                var query = _dbSet
                    .Include(pr => pr.User)
                    .Include(pr => pr.RequestedRole)
                    .Include(pr => pr.ProcessedByAdmin)
                    .AsNoTracking();

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
                    query = ApplySorting(query, "RequestedAt", false);
                }

                var items = await query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync(token);

                var totalCount = await query.CountAsync(token);

                return new PaginatedResult<PromotionRequest>
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
                _logger.LogWarning("GetAllAsync operation for {Entity} was canceled", typeof(PromotionRequest).Name);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting {Entity} list", typeof(PromotionRequest).Name);
                throw;
            }
        }

        public override async Task<PromotionRequest?> GetByIdAsync(int id, CancellationToken token = default)
        {
            try
            {
                return await _dbSet
                    .Include(pr => pr.User)
                    .Include(pr => pr.RequestedRole)
                    .Include(pr => pr.ProcessedByAdmin)
                    .FirstOrDefaultAsync(pr => pr.Id == id, token);
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning("GetByIdAsync for {Entity} with id {Id} was canceled", typeof(PromotionRequest).Name, id);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving {Entity} with id {Id}", typeof(PromotionRequest).Name, id);
                throw;
            }
        }

        public override async Task AddAsync(PromotionRequest entity, CancellationToken token = default)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                // If user is not admin, automatically set UserId to current user
                if (!IsCurrentUserAdmin())
                {
                    var currentUser = await GetCurrentUserAsync(token);
                    if (currentUser != null)
                    {
                        entity.UserId = currentUser.Id;
                    }
                    else
                    {
                        throw new InvalidOperationException("Unable to determine current user.");
                    }
                }

                entity.RequestedAt = DateTimeOffset.UtcNow;
                entity.Status = PromotionStatus.Pending;
                await _dbSet.AddAsync(entity, token);
                await _context.SaveChangesAsync(token);

                _logger.LogInformation("Added new {Entity}", typeof(PromotionRequest).Name);
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning("AddAsync for {Entity} was canceled", typeof(PromotionRequest).Name);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding {Entity}", typeof(PromotionRequest).Name);
                throw;
            }
        }

        public override async Task UpdateAsync(int id, PromotionRequest entity, CancellationToken token = default)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                var existingEntity = await GetByIdAsync(id, token);
                if (existingEntity == null)
                    throw new KeyNotFoundException($"{typeof(PromotionRequest).Name} with id {id} not found");

                // If admin is processing the promotion (status changed from Pending), set ProcessedByAdminId and ProcessedAt
                if (IsCurrentUserAdmin() && existingEntity.Status == PromotionStatus.Pending && entity.Status != PromotionStatus.Pending)
                {
                    var currentUser = await GetCurrentUserAsync(token);
                    if (currentUser != null)
                    {
                        entity.ProcessedByAdminId = currentUser.Id;
                        entity.ProcessedAt = DateTimeOffset.UtcNow;
                    }
                }

                // Preserve RequestedAt
                entity.RequestedAt = existingEntity.RequestedAt;

                _context.Entry(existingEntity).CurrentValues.SetValues(entity);
                _context.Entry(existingEntity).State = EntityState.Modified;

                await _context.SaveChangesAsync(token);

                _logger.LogInformation("Updated {Entity} with id {Id}", typeof(PromotionRequest).Name, id);
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning("UpdateAsync for {Entity} with id {Id} was canceled", typeof(PromotionRequest).Name, id);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating {Entity} with id {Id}", typeof(PromotionRequest).Name, id);
                throw;
            }
        }

        protected override IQueryable<PromotionRequest> ApplySearchFilter(IQueryable<PromotionRequest> query, string searchTerm)
        {
            return query.Where(pr =>
                (pr.User != null && (
                    pr.User.FirstName.Contains(searchTerm) ||
                    (pr.User.MiddleName != null && pr.User.MiddleName.Contains(searchTerm)) ||
                    (pr.User.LastName != null && pr.User.LastName.Contains(searchTerm)) ||
                    pr.User.Email.Contains(searchTerm)
                )) ||
                (pr.RequestedRole != null && pr.RequestedRole.Name.Contains(searchTerm)) ||
                (pr.Reason != null && pr.Reason.Contains(searchTerm)) ||
                pr.Status.ToString().Contains(searchTerm)
            );
        }

        protected override IQueryable<PromotionRequest> ApplySorting(IQueryable<PromotionRequest> query, string sortBy, bool ascending)
        {
            if (sortBy == "User.Email" || sortBy == "UserId")
            {
                query = ascending ? query.OrderBy(pr => pr.User != null ? pr.User.Email : "") 
                    : query.OrderByDescending(pr => pr.User != null ? pr.User.Email : "");
                return query;
            }
            if (sortBy == "RequestedRole.Name" || sortBy == "RequestedRoleId")
            {
                query = ascending ? query.OrderBy(pr => pr.RequestedRole != null ? pr.RequestedRole.Name : "") 
                    : query.OrderByDescending(pr => pr.RequestedRole != null ? pr.RequestedRole.Name : "");
                return query;
            }
            if (sortBy == "Status")
            {
                query = ascending ? query.OrderBy(pr => pr.Status) 
                    : query.OrderByDescending(pr => pr.Status);
                return query;
            }
            return base.ApplySorting(query, sortBy, ascending);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync(CancellationToken token = default)
        {
            try
            {
                return await _users
                    .OrderBy(u => u.LastName)
                    .ThenBy(u => u.FirstName)
                    .ToListAsync(token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting User list");
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

        public async Task<User?> GetCurrentUserAsync(CancellationToken token = default)
        {
            try
            {
                var user = _httpContextAccessor.HttpContext?.User;
                if (user == null)
                    return null;

                var userIdClaim = user.FindFirst("id") ?? user.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
                    return null;

                return await _users.FirstOrDefaultAsync(u => u.Id == userId, token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting current user");
                return null;
            }
        }

        public bool IsCurrentUserAdmin()
        {
            try
            {
                var user = _httpContextAccessor.HttpContext?.User;
                if (user == null)
                    return false;

                var roleIdClaim = user.FindFirst("roleId");
                if (roleIdClaim == null || !int.TryParse(roleIdClaim.Value, out int roleId))
                    return false;

                return roleId == (int)RoleType.Admin;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while checking if current user is admin");
                return false;
            }
        }
    }
}

