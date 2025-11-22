using ClinicManagement.Data;
using ClinicManagement.Models.Auth;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagement.Services.Auth
{
    public interface IPromotionRequestService : IService<PromotionRequest>
    {
        Task ApprovePromotionRequestAsync(int requestId, int adminId, CancellationToken token = default);
        Task RejectPromotionRequestAsync(int requestId, int adminId, CancellationToken token = default);
        Task<IEnumerable<Role>> GetAvailableRolesAsync(int userId, CancellationToken token = default);
        Task<PaginatedResult<PromotionRequest>> GetByUserIdAsync(
            int userId,
            int pageNumber = 1,
            int pageSize = 10,
            string? searchTerm = null,
            string? sortBy = null,
            bool sortAscending = true,
            CancellationToken token = default);
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
                    query = query.Where(pr =>
                        (pr.User != null && (
                            pr.User.FirstName.ToLower().Contains(searchTerm.ToLower()) ||
                            (pr.User.MiddleName != null && pr.User.MiddleName.ToLower().Contains(searchTerm.ToLower())) ||
                            (pr.User.LastName != null && pr.User.LastName.ToLower().Contains(searchTerm.ToLower())) ||
                            pr.User.Email.ToLower().Contains(searchTerm.ToLower())
                        )) ||
                        (pr.RequestedRole != null && pr.RequestedRole.Name.ToLower().Contains(searchTerm.ToLower())) ||
                        (pr.Reason != null && pr.Reason.ToLower().Contains(searchTerm.ToLower())) ||
                        pr.Status.ToString().ToLower().Contains(searchTerm.ToLower())
                    );
                }

                if (!string.IsNullOrWhiteSpace(sortBy))
                {
                    query = ApplySorting(query, sortBy, sortAscending);
                }
                else
                {
                    query = ApplySorting(query, "RequestedAt", false); // Default: newest first
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
                _logger.LogWarning("GetAllAsync operation for PromotionRequest was canceled");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting PromotionRequest list");
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
                _logger.LogWarning("GetByIdAsync for PromotionRequest with id {Id} was canceled", id);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving PromotionRequest with id {Id}", id);
                throw;
            }
        }

        public override async Task AddAsync(PromotionRequest entity, CancellationToken token = default)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                // Validate user exists
                var user = await _users.FindAsync([entity.UserId], token);
                if (user == null)
                    throw new ArgumentException("User not found.");

                // Validate requested role exists
                var role = await _roles.FindAsync([entity.RequestedRoleId], token);
                if (role == null)
                    throw new ArgumentException("Requested role not found.");

                // Check if user already has a pending request
                var existingPendingRequest = await _dbSet
                    .FirstOrDefaultAsync(pr => pr.UserId == entity.UserId && pr.Status == PromotionStatus.Pending, token);
                
                if (existingPendingRequest != null)
                    throw new ArgumentException("You already have a pending promotion request.");

                // Check if user is already at or above the requested role
                if (user.RoleId >= entity.RequestedRoleId)
                    throw new ArgumentException("You cannot request a role that is equal to or lower than your current role.");

                entity.RequestedAt = DateTimeOffset.UtcNow;
                entity.Status = PromotionStatus.Pending;
                entity.ProcessedByAdminId = null;
                entity.ProcessedAt = null;

                await base.AddAsync(entity, token);
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning("AddAsync for PromotionRequest was canceled");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding PromotionRequest");
                throw;
            }
        }

        public async Task ApprovePromotionRequestAsync(int requestId, int adminId, CancellationToken token = default)
        {
            try
            {
                var request = await _dbSet
                    .Include(pr => pr.User)
                    .FirstOrDefaultAsync(pr => pr.Id == requestId, token);

                if (request == null)
                    throw new KeyNotFoundException($"PromotionRequest with id {requestId} not found");

                if (request.Status != PromotionStatus.Pending)
                    throw new InvalidOperationException("Only pending promotion requests can be approved.");

                // Update user's role
                if (request.User != null)
                {
                    request.User.RoleId = request.RequestedRoleId;
                }

                // Update request status
                request.Status = PromotionStatus.Approved;
                request.ProcessedByAdminId = adminId;
                request.ProcessedAt = DateTimeOffset.UtcNow;

                await _context.SaveChangesAsync(token);

                _logger.LogInformation("Approved PromotionRequest with id {RequestId} by Admin {AdminId}", requestId, adminId);
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning("ApprovePromotionRequestAsync for request {RequestId} was canceled", requestId);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while approving PromotionRequest with id {RequestId}", requestId);
                throw;
            }
        }

        public async Task RejectPromotionRequestAsync(int requestId, int adminId, CancellationToken token = default)
        {
            try
            {
                var request = await _dbSet.FindAsync([requestId], token);
                if (request == null)
                    throw new KeyNotFoundException($"PromotionRequest with id {requestId} not found");

                if (request.Status != PromotionStatus.Pending)
                    throw new InvalidOperationException("Only pending promotion requests can be rejected.");

                request.Status = PromotionStatus.Rejected;
                request.ProcessedByAdminId = adminId;
                request.ProcessedAt = DateTimeOffset.UtcNow;

                await _context.SaveChangesAsync(token);

                _logger.LogInformation("Rejected PromotionRequest with id {RequestId} by Admin {AdminId}", requestId, adminId);
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning("RejectPromotionRequestAsync for request {RequestId} was canceled", requestId);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while rejecting PromotionRequest with id {RequestId}", requestId);
                throw;
            }
        }

        public async Task<IEnumerable<Role>> GetAvailableRolesAsync(int userId, CancellationToken token = default)
        {
            try
            {
                var user = await _users.FindAsync([userId], token);
                if (user == null)
                    throw new KeyNotFoundException($"User with id {userId} not found");

                // Return roles that are higher than the user's current role
                return await _roles
                    .AsNoTracking()
                    .Where(r => r.Id > user.RoleId)
                    .OrderBy(r => r.Id)
                    .ToListAsync(token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving available roles for User with id {UserId}", userId);
                throw;
            }
        }

        public async Task<PaginatedResult<PromotionRequest>> GetByUserIdAsync(
            int userId,
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
                    .Where(pr => pr.UserId == userId)
                    .AsNoTracking();

                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    query = query.Where(pr =>
                        (pr.RequestedRole != null && pr.RequestedRole.Name.ToLower().Contains(searchTerm.ToLower())) ||
                        (pr.Reason != null && pr.Reason.ToLower().Contains(searchTerm.ToLower())) ||
                        pr.Status.ToString().ToLower().Contains(searchTerm.ToLower())
                    );
                }

                if (!string.IsNullOrWhiteSpace(sortBy))
                {
                    query = ApplySorting(query, sortBy, sortAscending);
                }
                else
                {
                    query = ApplySorting(query, "RequestedAt", false); // Default: newest first
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
                _logger.LogWarning("GetByUserIdAsync operation for PromotionRequest was canceled");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting PromotionRequest list for User {UserId}", userId);
                throw;
            }
        }
    }
}

