using ClinicManagement.Data;
using ClinicManagement.Models.Health;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagement.Services.Health
{
    public interface ISicknessSymptomService : IService<SicknessSymptom>
    {
        Task RemoveAsync(int sicknessId, int symptomId, CancellationToken token = default);
        Task<SicknessSymptom?> GetByIdAsync(int sicknessId, int symptomId, CancellationToken token = default);
        Task<IEnumerable<Sickness>> GetAllSicknessesAsync(CancellationToken token = default);
        Task<IEnumerable<Symptom>> GetAllSymptomsAsync(CancellationToken token = default);
    }

    public class SicknessSymptomService : Service<SicknessSymptom>, ISicknessSymptomService
    {
        private readonly DbSet<Sickness> _sicknesses;
        private readonly DbSet<Symptom> _symptoms;

        public SicknessSymptomService(ClinicDbContext context, ILogger<SicknessSymptomService> logger)
            : base(context, logger)
        {
            _sicknesses = _context.Set<Sickness>();
            _symptoms = _context.Set<Symptom>();
        }

        public override async Task<PaginatedResult<SicknessSymptom>> GetAllAsync(
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
                var query = _dbSet.Include(ss => ss.Sickness).Include(ss => ss.Symptom).AsNoTracking();

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

                return new PaginatedResult<SicknessSymptom>
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
                _logger.LogWarning("GetAllAsync operation for {Entity} was canceled", typeof(SicknessSymptom).Name);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting {Entity} list", typeof(SicknessSymptom).Name);
                throw;
            }
        }

        public override async Task<SicknessSymptom?> GetByIdAsync(int id, CancellationToken token = default)
        {
            throw new NotSupportedException("SicknessSymptom uses composite key. Use GetByIdAsync(int sicknessId, int symptomId) instead.");
        }

        public async Task<SicknessSymptom?> GetByIdAsync(int sicknessId, int symptomId, CancellationToken token = default)
        {
            try
            {
                return await _dbSet
                    .Include(ss => ss.Sickness)
                    .Include(ss => ss.Symptom)
                    .FirstOrDefaultAsync(ss => ss.SicknessId == sicknessId && ss.SymptomId == symptomId, token);
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning("GetByIdAsync for {Entity} was canceled", typeof(SicknessSymptom).Name);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving {Entity}", typeof(SicknessSymptom).Name);
                throw;
            }
        }

        public override async Task AddAsync(SicknessSymptom entity, CancellationToken token = default)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                // Check if already exists
                var exists = await _dbSet.AnyAsync(ss => ss.SicknessId == entity.SicknessId && ss.SymptomId == entity.SymptomId, token);
                if (exists)
                    throw new InvalidOperationException("This relationship already exists.");

                await base.AddAsync(entity, token);
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning("AddAsync for {Entity} was canceled", typeof(SicknessSymptom).Name);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding {Entity}", typeof(SicknessSymptom).Name);
                throw;
            }
        }

        public override async Task UpdateAsync(int id, SicknessSymptom entity, CancellationToken token = default)
        {
            throw new NotSupportedException("SicknessSymptom uses composite key. Updates are not supported for relationship entities.");
        }

        public override async Task RemoveAsync(int id, CancellationToken token = default)
        {
            throw new NotSupportedException("SicknessSymptom uses composite key. Use RemoveAsync(int sicknessId, int symptomId) instead.");
        }

        public async Task RemoveAsync(int sicknessId, int symptomId, CancellationToken token = default)
        {
            try
            {
                var entity = await GetByIdAsync(sicknessId, symptomId, token);
                if (entity == null)
                    throw new KeyNotFoundException($"SicknessSymptom with SicknessId {sicknessId} and SymptomId {symptomId} not found");

                _dbSet.Remove(entity);
                await _context.SaveChangesAsync(token);
                _logger.LogInformation("Removed {Entity}", typeof(SicknessSymptom).Name);
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning("RemoveAsync for {Entity} was canceled", typeof(SicknessSymptom).Name);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while removing {Entity}", typeof(SicknessSymptom).Name);
                throw;
            }
        }

        protected override IQueryable<SicknessSymptom> ApplySearchFilter(IQueryable<SicknessSymptom> query, string searchTerm)
        {
            return query.Where(ss =>
                (ss.Sickness != null && ss.Sickness.Name.Contains(searchTerm)) ||
                (ss.Symptom != null && ss.Symptom.Name.Contains(searchTerm))
            );
        }

        protected override IQueryable<SicknessSymptom> ApplySorting(IQueryable<SicknessSymptom> query, string sortBy, bool ascending)
        {
            if (sortBy == "Sickness.Name")
            {
                query = ascending ? query.OrderBy(ss => ss.Sickness != null ? ss.Sickness.Name : "") 
                    : query.OrderByDescending(ss => ss.Sickness != null ? ss.Sickness.Name : "");
                return query;
            }
            if (sortBy == "Symptom.Name")
            {
                query = ascending ? query.OrderBy(ss => ss.Symptom != null ? ss.Symptom.Name : "") 
                    : query.OrderByDescending(ss => ss.Symptom != null ? ss.Symptom.Name : "");
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

        public async Task<IEnumerable<Symptom>> GetAllSymptomsAsync(CancellationToken token = default)
        {
            try
            {
                return await _symptoms.OrderBy(s => s.Name).ToListAsync(token);
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning("GetAllSymptomsAsync operation was canceled");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting Symptom list");
                throw;
            }
        }
    }
}

