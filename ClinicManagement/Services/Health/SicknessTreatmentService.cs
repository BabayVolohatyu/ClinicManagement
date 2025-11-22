using ClinicManagement.Data;
using ClinicManagement.Models.Health;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagement.Services.Health
{
    public interface ISicknessTreatmentService : IService<SicknessTreatment>
    {
        Task RemoveAsync(int sicknessId, int treatmentId, CancellationToken token = default);
        Task<SicknessTreatment?> GetByIdAsync(int sicknessId, int treatmentId, CancellationToken token = default);
        Task<IEnumerable<Sickness>> GetAllSicknessesAsync(CancellationToken token = default);
        Task<IEnumerable<Treatment>> GetAllTreatmentsAsync(CancellationToken token = default);
    }

    public class SicknessTreatmentService : Service<SicknessTreatment>, ISicknessTreatmentService
    {
        private readonly DbSet<Sickness> _sicknesses;
        private readonly DbSet<Treatment> _treatments;

        public SicknessTreatmentService(ClinicDbContext context, ILogger<SicknessTreatmentService> logger)
            : base(context, logger)
        {
            _sicknesses = _context.Set<Sickness>();
            _treatments = _context.Set<Treatment>();
        }

        public override async Task<PaginatedResult<SicknessTreatment>> GetAllAsync(
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
                var query = _dbSet.Include(st => st.Sickness).Include(st => st.Treatment).AsNoTracking();

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

                return new PaginatedResult<SicknessTreatment>
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
                _logger.LogWarning("GetAllAsync operation for {Entity} was canceled", typeof(SicknessTreatment).Name);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting {Entity} list", typeof(SicknessTreatment).Name);
                throw;
            }
        }

        public override async Task<SicknessTreatment?> GetByIdAsync(int id, CancellationToken token = default)
        {
            throw new NotSupportedException("SicknessTreatment uses composite key. Use GetByIdAsync(int sicknessId, int treatmentId) instead.");
        }

        public async Task<SicknessTreatment?> GetByIdAsync(int sicknessId, int treatmentId, CancellationToken token = default)
        {
            try
            {
                return await _dbSet
                    .Include(st => st.Sickness)
                    .Include(st => st.Treatment)
                    .FirstOrDefaultAsync(st => st.SicknessId == sicknessId && st.TreatmentId == treatmentId, token);
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning("GetByIdAsync for {Entity} was canceled", typeof(SicknessTreatment).Name);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving {Entity}", typeof(SicknessTreatment).Name);
                throw;
            }
        }

        public override async Task AddAsync(SicknessTreatment entity, CancellationToken token = default)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                var exists = await _dbSet.AnyAsync(st => st.SicknessId == entity.SicknessId && st.TreatmentId == entity.TreatmentId, token);
                if (exists)
                    throw new InvalidOperationException("This relationship already exists.");

                await base.AddAsync(entity, token);
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning("AddAsync for {Entity} was canceled", typeof(SicknessTreatment).Name);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding {Entity}", typeof(SicknessTreatment).Name);
                throw;
            }
        }

        public override async Task UpdateAsync(int id, SicknessTreatment entity, CancellationToken token = default)
        {
            throw new NotSupportedException("SicknessTreatment uses composite key. Updates are not supported for relationship entities.");
        }

        public override async Task RemoveAsync(int id, CancellationToken token = default)
        {
            throw new NotSupportedException("SicknessTreatment uses composite key. Use RemoveAsync(int sicknessId, int treatmentId) instead.");
        }

        public async Task RemoveAsync(int sicknessId, int treatmentId, CancellationToken token = default)
        {
            try
            {
                var entity = await GetByIdAsync(sicknessId, treatmentId, token);
                if (entity == null)
                    throw new KeyNotFoundException($"SicknessTreatment with SicknessId {sicknessId} and TreatmentId {treatmentId} not found");

                _dbSet.Remove(entity);
                await _context.SaveChangesAsync(token);
                _logger.LogInformation("Removed {Entity}", typeof(SicknessTreatment).Name);
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning("RemoveAsync for {Entity} was canceled", typeof(SicknessTreatment).Name);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while removing {Entity}", typeof(SicknessTreatment).Name);
                throw;
            }
        }

        protected override IQueryable<SicknessTreatment> ApplySearchFilter(IQueryable<SicknessTreatment> query, string searchTerm)
        {
            return query.Where(st =>
                (st.Sickness != null && st.Sickness.Name.Contains(searchTerm)) ||
                (st.Treatment != null && st.Treatment.Name.Contains(searchTerm))
            );
        }

        protected override IQueryable<SicknessTreatment> ApplySorting(IQueryable<SicknessTreatment> query, string sortBy, bool ascending)
        {
            if (sortBy == "Sickness.Name")
            {
                query = ascending ? query.OrderBy(st => st.Sickness != null ? st.Sickness.Name : "") 
                    : query.OrderByDescending(st => st.Sickness != null ? st.Sickness.Name : "");
                return query;
            }
            if (sortBy == "Treatment.Name")
            {
                query = ascending ? query.OrderBy(st => st.Treatment != null ? st.Treatment.Name : "") 
                    : query.OrderByDescending(st => st.Treatment != null ? st.Treatment.Name : "");
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

        public async Task<IEnumerable<Treatment>> GetAllTreatmentsAsync(CancellationToken token = default)
        {
            try
            {
                return await _treatments.OrderBy(t => t.Name).ToListAsync(token);
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning("GetAllTreatmentsAsync operation was canceled");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting Treatment list");
                throw;
            }
        }
    }
}

