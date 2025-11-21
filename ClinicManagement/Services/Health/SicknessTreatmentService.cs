using ClinicManagement.Data;
using ClinicManagement.Models.Health;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagement.Services.Health
{
    public interface ISicknessTreatmentService
    {
        Task AddAsync(SicknessTreatment entity, CancellationToken token = default);
        Task RemoveAsync(int sicknessId, int treatmentId, CancellationToken token = default);
        Task<SicknessTreatment?> GetByIdAsync(int sicknessId, int treatmentId, CancellationToken token = default);
        Task<PaginatedResult<SicknessTreatment>> GetAllAsync(
            int pageNumber = 1,
            int pageSize = 10,
            string? searchTerm = null,
            string? sortBy = null,
            bool sortAscending = true,
            CancellationToken token = default);
        Task<IEnumerable<Sickness>> GetAllSicknessesAsync(CancellationToken token = default);
        Task<IEnumerable<Treatment>> GetAllTreatmentsAsync(CancellationToken token = default);
    }

    public class SicknessTreatmentService : ISicknessTreatmentService
    {
        protected readonly ClinicDbContext _context;
        protected readonly DbSet<SicknessTreatment> _dbSet;
        protected readonly ILogger<SicknessTreatmentService> _logger;
        private readonly DbSet<Sickness> _sicknesses;
        private readonly DbSet<Treatment> _treatments;

        public SicknessTreatmentService(ClinicDbContext context, ILogger<SicknessTreatmentService> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<SicknessTreatment>();
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _sicknesses = _context.Set<Sickness>();
            _treatments = _context.Set<Treatment>();
        }

        public async Task<PaginatedResult<SicknessTreatment>> GetAllAsync(
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
                    query = query.Where(st =>
                        (st.Sickness != null && st.Sickness.Name.Contains(searchTerm)) ||
                        (st.Treatment != null && st.Treatment.Name.Contains(searchTerm))
                    );
                }

                if (!string.IsNullOrWhiteSpace(sortBy))
                {
                    if (sortBy == "Sickness.Name")
                    {
                        query = sortAscending ? query.OrderBy(st => st.Sickness != null ? st.Sickness.Name : "") : query.OrderByDescending(st => st.Sickness != null ? st.Sickness.Name : "");
                    }
                    else if (sortBy == "Treatment.Name")
                    {
                        query = sortAscending ? query.OrderBy(st => st.Treatment != null ? st.Treatment.Name : "") : query.OrderByDescending(st => st.Treatment != null ? st.Treatment.Name : "");
                    }
                }
                else
                {
                    query = query.OrderBy(st => st.SicknessId).ThenBy(st => st.TreatmentId);
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting SicknessTreatment list");
                throw;
            }
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving SicknessTreatment");
                throw;
            }
        }

        public async Task AddAsync(SicknessTreatment entity, CancellationToken token = default)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                var exists = await _dbSet.AnyAsync(st => st.SicknessId == entity.SicknessId && st.TreatmentId == entity.TreatmentId, token);
                if (exists)
                    throw new InvalidOperationException("This relationship already exists.");

                await _dbSet.AddAsync(entity, token);
                await _context.SaveChangesAsync(token);
                _logger.LogInformation("Added new SicknessTreatment");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding SicknessTreatment");
                throw;
            }
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
                _logger.LogInformation("Removed SicknessTreatment");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while removing SicknessTreatment");
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

        public async Task<IEnumerable<Treatment>> GetAllTreatmentsAsync(CancellationToken token = default)
        {
            try
            {
                return await _treatments.OrderBy(t => t.Name).ToListAsync(token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting Treatment list");
                throw;
            }
        }
    }
}

