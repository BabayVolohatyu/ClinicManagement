using ClinicManagement.Data;
using ClinicManagement.Models.Auth;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagement.Services.Auth
{
    public interface IPromotionRequestService : IService<PromotionRequest>
    {
        Task<IEnumerable<User>> GetAllUsersAsync(CancellationToken token = default);
        Task<IEnumerable<Role>> GetAllRolesAsync(CancellationToken token = default);
    }

    public class PromotionRequestService : Service<PromotionRequest>, IPromotionRequestService
    {
        private readonly DbSet<User> _users;
        private readonly DbSet<Role> _roles;

        public PromotionRequestService(ClinicDbContext context, ILogger<PromotionRequestService> logger)
            : base(context, logger)
        {
            _users = _context.Set<User>();
            _roles = _context.Set<Role>();
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
                if (entity.UserId == 0)
                {
                    throw new ArgumentException("UserId cannot be 0.");
                }

                if (entity.RequestedRoleId == 0)
                {
                    throw new ArgumentException("RequestedRoleId cannot be 0.");
                }

                // Check if user already has a pending promotion request
                var existingPendingRequest = await _dbSet
                    .FirstOrDefaultAsync(pr => pr.UserId == entity.UserId && pr.Status == PromotionStatus.Pending, token);

                if (existingPendingRequest != null)
                {
                    throw new InvalidOperationException("You already have a pending promotion request. Please wait for it to be processed before submitting a new one.");
                }

                await base.AddAsync(entity, token);
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
                if (entity.UserId == 0)
                {
                    throw new ArgumentException("UserId cannot be 0.");
                }

                if (entity.RequestedRoleId == 0)
                {
                    throw new ArgumentException("RequestedRoleId cannot be 0.");
                }

                var existingEntity = await GetByIdAsync(id, token);
                if (existingEntity == null)
                    throw new KeyNotFoundException($"{typeof(PromotionRequest).Name} with id {id} not found");

                // Preserve original values that shouldn't be changed
                var originalRequestedAt = existingEntity.RequestedAt;
                var originalStatus = existingEntity.Status;

                // If status is changing from Pending to Approved/Rejected, update user's role and set processed info
                if (originalStatus == PromotionStatus.Pending && entity.Status != PromotionStatus.Pending)
                {
                    // Set processed info (should be set by controller, but ensure it's set)
                    if (!entity.ProcessedAt.HasValue)
                    {
                        entity.ProcessedAt = DateTimeOffset.UtcNow;
                    }

                    // If approved, update the user's role
                    if (entity.Status == PromotionStatus.Approved)
                    {
                        var user = await _users.FindAsync(new object[] { entity.UserId }, token);
                        if (user != null)
                        {
                            user.RoleId = entity.RequestedRoleId;
                            _users.Update(user);
                            _logger.LogInformation("Updated user {UserId} role to {RoleId} after promotion approval", entity.UserId, entity.RequestedRoleId);
                        }
                        else
                        {
                            throw new InvalidOperationException($"User with id {entity.UserId} not found.");
                        }
                    }
                }

                // Preserve RequestedAt
                entity.RequestedAt = originalRequestedAt;

                // Clear navigation properties to prevent EF Core from trying to insert them
                entity.RequestedRole = null!;
                entity.User = null!;
                entity.ProcessedByAdmin = null;

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
    }
}

