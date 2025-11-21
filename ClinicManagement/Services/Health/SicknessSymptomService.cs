using ClinicManagement.Data;
using ClinicManagement.Models.Health;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagement.Services.Health
{
    public interface ISicknessSymptomService
    {
        Task AddAsync(SicknessSymptom entity, CancellationToken token = default);
        Task RemoveAsync(int sicknessId, int symptomId, CancellationToken token = default);
        Task<SicknessSymptom?> GetByIdAsync(int sicknessId, int symptomId, CancellationToken token = default);
        Task<PaginatedResult<SicknessSymptom>> GetAllAsync(
            int pageNumber = 1,
            int pageSize = 10,
            string? searchTerm = null,
            string? sortBy = null,
            bool sortAscending = true,
            CancellationToken token = default);
        Task<IEnumerable<Sickness>> GetAllSicknessesAsync(CancellationToken token = default);
        Task<IEnumerable<Symptom>> GetAllSymptomsAsync(CancellationToken token = default);
    }

    public class SicknessSymptomService : ISicknessSymptomService
    {
        protected readonly ClinicDbContext _context;
        protected readonly DbSet<SicknessSymptom> _dbSet;
        protected readonly ILogger<SicknessSymptomService> _logger;
        private readonly DbSet<Sickness> _sicknesses;
        private readonly DbSet<Symptom> _symptoms;

        public SicknessSymptomService(ClinicDbContext context, ILogger<SicknessSymptomService> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<SicknessSymptom>();
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _sicknesses = _context.Set<Sickness>();
            _symptoms = _context.Set<Symptom>();
        }

        public async Task<PaginatedResult<SicknessSymptom>> GetAllAsync(
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
                    query = query.Where(ss =>
                        (ss.Sickness != null && ss.Sickness.Name.Contains(searchTerm)) ||
                        (ss.Symptom != null && ss.Symptom.Name.Contains(searchTerm))
                    );
                }

                if (!string.IsNullOrWhiteSpace(sortBy))
                {
                    if (sortBy == "Sickness.Name")
                    {
                        query = sortAscending ? query.OrderBy(ss => ss.Sickness != null ? ss.Sickness.Name : "") : query.OrderByDescending(ss => ss.Sickness != null ? ss.Sickness.Name : "");
                    }
                    else if (sortBy == "Symptom.Name")
                    {
                        query = sortAscending ? query.OrderBy(ss => ss.Symptom != null ? ss.Symptom.Name : "") : query.OrderByDescending(ss => ss.Symptom != null ? ss.Symptom.Name : "");
                    }
                }
                else
                {
                    query = query.OrderBy(ss => ss.SicknessId).ThenBy(ss => ss.SymptomId);
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting SicknessSymptom list");
                throw;
            }
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving SicknessSymptom");
                throw;
            }
        }

        public async Task AddAsync(SicknessSymptom entity, CancellationToken token = default)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                // Check if already exists
                var exists = await _dbSet.AnyAsync(ss => ss.SicknessId == entity.SicknessId && ss.SymptomId == entity.SymptomId, token);
                if (exists)
                    throw new InvalidOperationException("This relationship already exists.");

                await _dbSet.AddAsync(entity, token);
                await _context.SaveChangesAsync(token);
                _logger.LogInformation("Added new SicknessSymptom");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding SicknessSymptom");
                throw;
            }
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
                _logger.LogInformation("Removed SicknessSymptom");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while removing SicknessSymptom");
                throw;
            }
        }

        public async Task<IEnumerable<Sickness>> GetAllSicknessesAsync(CancellationToken token = default)
        {
            try
            {
                return await _sicknesses.OrderBy(s => s.Name).ToListAsync(token);
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting Symptom list");
                throw;
            }
        }
    }
}

