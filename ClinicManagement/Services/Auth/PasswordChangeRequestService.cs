using ClinicManagement.Data;
using ClinicManagement.Models.Auth;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagement.Services.Auth
{
    public interface IPasswordChangeRequestService : IService<PasswordChangeRequest>
    {
        Task<IEnumerable<User>> GetAllUsersAsync(CancellationToken token = default);
    }

    public class PasswordChangeRequestService : Service<PasswordChangeRequest>, IPasswordChangeRequestService
    {
        private readonly DbSet<User> _users;
        private readonly IAuthService _authService;

        public PasswordChangeRequestService(ClinicDbContext context, IAuthService authService, ILogger<PasswordChangeRequestService> logger)
            : base(context, logger)
        {
            _users = _context.Set<User>();
            _authService = authService;
        }

        public override async Task<PaginatedResult<PasswordChangeRequest>> GetAllAsync(
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
                    .Include(pcr => pcr.User)
                        .ThenInclude(u => u.Role)
                    .Include(pcr => pcr.ProcessedByAdmin)
                        .ThenInclude(a => a.Role)
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

                var totalCount = await query.CountAsync(token);

                var items = await query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync(token);

                
                foreach (var item in items)
                {
                    if (item.User != null)
                        item.User.PasswordHash = string.Empty;
                    if (item.ProcessedByAdmin != null)
                        item.ProcessedByAdmin.PasswordHash = string.Empty;
                }

                return new PaginatedResult<PasswordChangeRequest>
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
                _logger.LogWarning("GetAllAsync operation for {Entity} was canceled", typeof(PasswordChangeRequest).Name);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting {Entity} list", typeof(PasswordChangeRequest).Name);
                throw;
            }
        }

        public override async Task<PasswordChangeRequest?> GetByIdAsync(int id, CancellationToken token = default)
        {
            try
            {
                var request = await _dbSet
                    .Include(pcr => pcr.User)
                        .ThenInclude(u => u.Role)
                    .Include(pcr => pcr.ProcessedByAdmin)
                        .ThenInclude(a => a.Role)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(pcr => pcr.Id == id, token);

                if (request != null)
                {
                    
                    request.User.PasswordHash = string.Empty;
                    if (request.ProcessedByAdmin != null)
                    {
                        request.ProcessedByAdmin.PasswordHash = string.Empty;
                    }
                }

                return request;
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning("GetByIdAsync for {Entity} with id {Id} was canceled", typeof(PasswordChangeRequest).Name, id);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving {Entity} with id {Id}", typeof(PasswordChangeRequest).Name, id);
                throw;
            }
        }

        public override async Task AddAsync(PasswordChangeRequest entity, CancellationToken token = default)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                if (entity.UserId == 0)
                {
                    throw new ArgumentException("UserId cannot be 0.");
                }

                
                var existingPendingRequest = await _dbSet
                    .FirstOrDefaultAsync(pcr => pcr.UserId == entity.UserId && pcr.Status == PasswordChangeStatus.Pending, token);

                if (existingPendingRequest != null)
                {
                    throw new InvalidOperationException("You already have a pending password change request. Please wait for it to be processed before submitting a new one.");
                }

                await base.AddAsync(entity, token);
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning("AddAsync for {Entity} was canceled", typeof(PasswordChangeRequest).Name);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding {Entity}", typeof(PasswordChangeRequest).Name);
                throw;
            }
        }

        public override async Task UpdateAsync(int id, PasswordChangeRequest entity, CancellationToken token = default)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                if (entity.UserId == 0)
                {
                    throw new ArgumentException("UserId cannot be 0.");
                }

                var existingEntity = await _dbSet
                    .FirstOrDefaultAsync(pcr => pcr.Id == id, token);
                if (existingEntity == null)
                    throw new KeyNotFoundException($"{typeof(PasswordChangeRequest).Name} with id {id} not found");

                
                var originalRequestedAt = existingEntity.RequestedAt;
                var originalStatus = existingEntity.Status;

                
                if (originalStatus == PasswordChangeStatus.Pending && entity.Status == PasswordChangeStatus.Approved)
                {
                    
                    if (!entity.ProcessedAt.HasValue)
                    {
                        entity.ProcessedAt = DateTimeOffset.UtcNow;
                    }

                    
                    var user = await _users.FindAsync(new object[] { entity.UserId }, token);
                    if (user != null)
                    {
                        
                        user.PasswordHash = existingEntity.RequestedPasswordHash;
                        _users.Update(user);
                        await _context.SaveChangesAsync(token);
                        _logger.LogInformation("Password changed for user {UserId} after password change request approval", entity.UserId);
                    }
                    else
                    {
                        throw new InvalidOperationException($"User with id {entity.UserId} not found.");
                    }
                }

                
                entity.RequestedAt = originalRequestedAt;
                entity.RequestedPasswordHash = existingEntity.RequestedPasswordHash;

                
                entity.User = null!;
                entity.ProcessedByAdmin = null;

                _context.Entry(existingEntity).CurrentValues.SetValues(entity);
                _context.Entry(existingEntity).State = EntityState.Modified;

                await _context.SaveChangesAsync(token);

                _logger.LogInformation("Updated {Entity} with id {Id}", typeof(PasswordChangeRequest).Name, id);
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning("UpdateAsync for {Entity} with id {Id} was canceled", typeof(PasswordChangeRequest).Name, id);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating {Entity} with id {Id}", typeof(PasswordChangeRequest).Name, id);
                throw;
            }
        }


        protected override IQueryable<PasswordChangeRequest> ApplySearchFilter(IQueryable<PasswordChangeRequest> query, string searchTerm)
        {
            return query.Where(pcr =>
                (pcr.User != null && (
                    pcr.User.FirstName.Contains(searchTerm) ||
                    (pcr.User.MiddleName != null && pcr.User.MiddleName.Contains(searchTerm)) ||
                    (pcr.User.LastName != null && pcr.User.LastName.Contains(searchTerm)) ||
                    pcr.User.Email.Contains(searchTerm)
                )) ||
                pcr.Status.ToString().Contains(searchTerm)
            );
        }

        protected override IQueryable<PasswordChangeRequest> ApplySorting(IQueryable<PasswordChangeRequest> query, string sortBy, bool ascending)
        {
            if (sortBy == "User.Email" || sortBy == "UserId")
            {
                query = ascending ? query.OrderBy(pcr => pcr.User != null ? pcr.User.Email : "")
                    : query.OrderByDescending(pcr => pcr.User != null ? pcr.User.Email : "");
                return query;
            }
            if (sortBy == "Status")
            {
                query = ascending ? query.OrderBy(pcr => pcr.Status)
                    : query.OrderByDescending(pcr => pcr.Status);
                return query;
            }
            return base.ApplySorting(query, sortBy, ascending);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync(CancellationToken token = default)
        {
            try
            {
                return await _users
                    .Select(u => new User
                    {
                        Id = u.Id,
                        FirstName = u.FirstName,
                        MiddleName = u.MiddleName,
                        LastName = u.LastName,
                        Email = u.Email,
                        CreatedAt = u.CreatedAt,
                        RoleId = u.RoleId,
                        Role = u.Role,
                        PasswordHash = string.Empty 
                    })
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
    }
}

