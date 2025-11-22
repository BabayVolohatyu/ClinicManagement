using ClinicManagement.Data;
using ClinicManagement.Models.Health;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagement.Services.Health
{
    public interface ISicknessProcedureService : IService<SicknessProcedure>
    {
        Task RemoveAsync(int sicknessId, int procedureId, CancellationToken token = default);
        Task<SicknessProcedure?> GetByIdAsync(int sicknessId, int procedureId, CancellationToken token = default);
        Task<IEnumerable<Sickness>> GetAllSicknessesAsync(CancellationToken token = default);
        Task<IEnumerable<Procedure>> GetAllProceduresAsync(CancellationToken token = default);
    }

    public class SicknessProcedureService : Service<SicknessProcedure>, ISicknessProcedureService
    {
        private readonly DbSet<Sickness> _sicknesses;
        private readonly DbSet<Procedure> _procedures;

        public SicknessProcedureService(ClinicDbContext context, ILogger<SicknessProcedureService> logger)
            : base(context, logger)
        {
            _sicknesses = _context.Set<Sickness>();
            _procedures = _context.Set<Procedure>();
        }

        public override async Task<PaginatedResult<SicknessProcedure>> GetAllAsync(
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
                var query = _dbSet.Include(sp => sp.Sickness).Include(sp => sp.Procedure).AsNoTracking();

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
                    query = ApplySorting(query, "SicknessId", true);
                }

                var items = await query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync(token);

                var totalCount = await query.CountAsync(token);

                return new PaginatedResult<SicknessProcedure>
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
                _logger.LogWarning("GetAllAsync operation for {Entity} was canceled", typeof(SicknessProcedure).Name);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting {Entity} list", typeof(SicknessProcedure).Name);
                throw;
            }
        }

        public override Task<SicknessProcedure?> GetByIdAsync(int id, CancellationToken token = default)
        {
            return Task.FromException<SicknessProcedure?>(new NotSupportedException("SicknessProcedure uses composite key. Use GetByIdAsync(int sicknessId, int procedureId) instead."));
        }

        public async Task<SicknessProcedure?> GetByIdAsync(int sicknessId, int procedureId, CancellationToken token = default)
        {
            try
            {
                return await _dbSet
                    .Include(sp => sp.Sickness)
                    .Include(sp => sp.Procedure)
                    .FirstOrDefaultAsync(sp => sp.SicknessId == sicknessId && sp.ProcedureId == procedureId, token);
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning("GetByIdAsync for {Entity} was canceled", typeof(SicknessProcedure).Name);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving {Entity}", typeof(SicknessProcedure).Name);
                throw;
            }
        }

        public override async Task AddAsync(SicknessProcedure entity, CancellationToken token = default)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                var exists = await _dbSet.AnyAsync(sp => sp.SicknessId == entity.SicknessId && sp.ProcedureId == entity.ProcedureId, token);
                if (exists)
                    throw new InvalidOperationException("This relationship already exists.");

                await base.AddAsync(entity, token);
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning("AddAsync for {Entity} was canceled", typeof(SicknessProcedure).Name);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding {Entity}", typeof(SicknessProcedure).Name);
                throw;
            }
        }

        public override Task UpdateAsync(int id, SicknessProcedure entity, CancellationToken token = default)
        {
            return Task.FromException(new NotSupportedException("SicknessProcedure uses composite key. Updates are not supported for relationship entities."));
        }

        public override Task RemoveAsync(int id, CancellationToken token = default)
        {
            return Task.FromException(new NotSupportedException("SicknessProcedure uses composite key. Use RemoveAsync(int sicknessId, int procedureId) instead."));
        }

        public async Task RemoveAsync(int sicknessId, int procedureId, CancellationToken token = default)
        {
            try
            {
                var entity = await GetByIdAsync(sicknessId, procedureId, token);
                if (entity == null)
                    throw new KeyNotFoundException($"SicknessProcedure with SicknessId {sicknessId} and ProcedureId {procedureId} not found");

                _dbSet.Remove(entity);
                await _context.SaveChangesAsync(token);
                _logger.LogInformation("Removed {Entity}", typeof(SicknessProcedure).Name);
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning("RemoveAsync for {Entity} was canceled", typeof(SicknessProcedure).Name);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while removing {Entity}", typeof(SicknessProcedure).Name);
                throw;
            }
        }

        protected override IQueryable<SicknessProcedure> ApplySearchFilter(IQueryable<SicknessProcedure> query, string searchTerm)
        {
            return query.Where(sp =>
                (sp.Sickness != null && sp.Sickness.Name.Contains(searchTerm)) ||
                (sp.Procedure != null && sp.Procedure.Name.Contains(searchTerm))
            );
        }

        protected override IQueryable<SicknessProcedure> ApplySorting(IQueryable<SicknessProcedure> query, string sortBy, bool ascending)
        {
            if (sortBy == "Sickness.Name")
            {
                query = ascending ? query.OrderBy(sp => sp.Sickness != null ? sp.Sickness.Name : "") 
                    : query.OrderByDescending(sp => sp.Sickness != null ? sp.Sickness.Name : "");
                return query;
            }
            if (sortBy == "Procedure.Name")
            {
                query = ascending ? query.OrderBy(sp => sp.Procedure != null ? sp.Procedure.Name : "") 
                    : query.OrderByDescending(sp => sp.Procedure != null ? sp.Procedure.Name : "");
                return query;
            }
            return base.ApplySorting(query, sortBy, ascending);
        }

        public async Task<IEnumerable<Sickness>> GetAllSicknessesAsync(CancellationToken token = default)
        {
            try
            {
                return await _sicknesses.OrderBy(s => s.Name).ToListAsync(token);
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning("GetAllSicknessesAsync operation was canceled");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting Sickness list");
                throw;
            }
        }

        public async Task<IEnumerable<Procedure>> GetAllProceduresAsync(CancellationToken token = default)
        {
            try
            {
                return await _procedures.OrderBy(p => p.Name).ToListAsync(token);
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning("GetAllProceduresAsync operation was canceled");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting Procedure list");
                throw;
            }
        }
    }
}

