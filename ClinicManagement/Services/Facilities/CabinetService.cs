using ClinicManagement.Data;
using ClinicManagement.Models.Facilities;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagement.Services.Facilities
{
    public interface ICabinetService : IService<Cabinet>
    {
        Task ValidateTypeExistsAsync(int typeId, CancellationToken token = default);
        Task<int> GetOrCreateTypeIdAsync(string typeName, CancellationToken token = default);
        Task<IEnumerable<CabinetType>> GetAllTypesAsync(CancellationToken token = default);
    }

    public class CabinetService : Service<Cabinet>, ICabinetService
    {
        private readonly DbSet<CabinetType> _types;

        public CabinetService(ClinicDbContext context, ILogger<CabinetService> logger)
            : base(context, logger)
        {
            _types = _context.Set<CabinetType>();
        }

        public async Task ValidateTypeExistsAsync(int typeId, CancellationToken token = default)
        {
            var exists = await _types.AnyAsync(t => t.Id == typeId, token);
            if (!exists)
                throw new ArgumentException($"CabinetType with Id {typeId} does not exist.");
        }

        public override async Task<PaginatedResult<Cabinet>> GetAllAsync(
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
                var query = _dbSet.Include(c => c.Type).AsNoTracking();

                // Apply filtration if search term is provided
                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    query = ApplySearchFilter(query, searchTerm);
                }

                // Apply sorting if sort field is provided
                if (!string.IsNullOrWhiteSpace(sortBy))
                {
                    query = ApplySorting(query, sortBy, sortAscending);
                }
                else
                {
                    // Default sorting by ID if no sort specified
                    query = ApplySorting(query, "Id", true);
                }

                var items = await query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync(token);

                var totalCount = await query.CountAsync(token);

                return new PaginatedResult<Cabinet>
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
                _logger.LogWarning("GetAllAsync operation for {Entity} was canceled", typeof(Cabinet).Name);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting {Entity} list", typeof(Cabinet).Name);
                throw;
            }
        }

        public override async Task<Cabinet?> GetByIdAsync(int id, CancellationToken token = default)
        {
            try
            {
                return await _dbSet
                    .Include(c => c.Type)
                    .FirstOrDefaultAsync(c => c.Id == id, token);
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning("GetByIdAsync for {Entity} with id {Id} was canceled", typeof(Cabinet).Name, id);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving {Entity} with id {Id}", typeof(Cabinet).Name, id);
                throw;
            }
        }

        protected override IQueryable<Cabinet> ApplySearchFilter(IQueryable<Cabinet> query, string searchTerm)
        {
            return query.Where(c =>
                c.Building.ToString().Contains(searchTerm) ||
                c.Floor.ToString().Contains(searchTerm) ||
                c.Number.ToString().Contains(searchTerm) ||
                (c.Type != null && c.Type.Type.Contains(searchTerm))
            );
        }

        protected override IQueryable<Cabinet> ApplySorting(IQueryable<Cabinet> query, string sortBy, bool ascending)
        {
            if (sortBy == "Type.Type")
            {
                query = ascending ? query.OrderBy(c => c.Type.Type) : query.OrderByDescending(c => c.Type.Type);
                return query;
            }
            return base.ApplySorting(query, sortBy, ascending);
        }

        public async Task<int> GetOrCreateTypeIdAsync(string typeName, CancellationToken token = default)
        {
            if (string.IsNullOrWhiteSpace(typeName))
                throw new ArgumentException("Type name cannot be null or empty.", nameof(typeName));

            var existingType = await _types
                .FirstOrDefaultAsync(t => t.Type.ToLower() == typeName.ToLower(), token);

            if (existingType != null)
                return existingType.Id;

            // Create new type
            var newType = new CabinetType { Type = typeName.Trim() };
            await _types.AddAsync(newType, token);
            await _context.SaveChangesAsync(token);

            _logger.LogInformation("Created new CabinetType: {TypeName}", typeName);
            return newType.Id;
        }

        public async Task<IEnumerable<CabinetType>> GetAllTypesAsync(CancellationToken token = default)
        {
            try
            {
                return await _types
                    .OrderBy(t => t.Type)
                    .ToListAsync(token);
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning("GetAllTypesAsync operation was canceled");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting CabinetType list");
                throw;
            }
        }

        public override async Task AddAsync(Cabinet entity, CancellationToken token = default)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                // If TypeId is 0 or invalid, we need to handle it via typeName
                // This will be handled in the controller
                if (entity.TypeId == 0)
                {
                    throw new ArgumentException("TypeId cannot be 0. Use GetOrCreateTypeIdAsync to resolve type first.");
                }

                await ValidateTypeExistsAsync(entity.TypeId, token);
                await base.AddAsync(entity, token);
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning("AddAsync for {Entity} was canceled", typeof(Cabinet).Name);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding {Entity}", typeof(Cabinet).Name);
                throw;
            }
        }

        public override async Task UpdateAsync(int id, Cabinet entity, CancellationToken token = default)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                // If TypeId is 0 or invalid, we need to handle it via typeName
                // This will be handled in the controller
                if (entity.TypeId == 0)
                {
                    throw new ArgumentException("TypeId cannot be 0. Use GetOrCreateTypeIdAsync to resolve type first.");
                }

                await ValidateTypeExistsAsync(entity.TypeId, token);
                await base.UpdateAsync(id, entity, token);
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning("UpdateAsync for {Entity} with id {Id} was canceled", typeof(Cabinet).Name, id);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating {Entity} with id {Id}", typeof(Cabinet).Name, id);
                throw;
            }
        }
    }
}
