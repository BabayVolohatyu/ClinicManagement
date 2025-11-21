using ClinicManagement.Data;
using ClinicManagement.Models.Health;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagement.Services.Health
{
    public interface ISicknessProcedureService
    {
        Task AddAsync(SicknessProcedure entity, CancellationToken token = default);
        Task RemoveAsync(int sicknessId, int procedureId, CancellationToken token = default);
        Task<SicknessProcedure?> GetByIdAsync(int sicknessId, int procedureId, CancellationToken token = default);
        Task<PaginatedResult<SicknessProcedure>> GetAllAsync(
            int pageNumber = 1,
            int pageSize = 10,
            string? searchTerm = null,
            string? sortBy = null,
            bool sortAscending = true,
            CancellationToken token = default);
        Task<IEnumerable<Sickness>> GetAllSicknessesAsync(CancellationToken token = default);
        Task<IEnumerable<Procedure>> GetAllProceduresAsync(CancellationToken token = default);
    }

    public class SicknessProcedureService : ISicknessProcedureService
    {
        protected readonly ClinicDbContext _context;
        protected readonly DbSet<SicknessProcedure> _dbSet;
        protected readonly ILogger<SicknessProcedureService> _logger;
        private readonly DbSet<Sickness> _sicknesses;
        private readonly DbSet<Procedure> _procedures;

        public SicknessProcedureService(ClinicDbContext context, ILogger<SicknessProcedureService> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<SicknessProcedure>();
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _sicknesses = _context.Set<Sickness>();
            _procedures = _context.Set<Procedure>();
        }

        public async Task<PaginatedResult<SicknessProcedure>> GetAllAsync(
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
                    query = query.Where(sp =>
                        (sp.Sickness != null && sp.Sickness.Name.Contains(searchTerm)) ||
                        (sp.Procedure != null && sp.Procedure.Name.Contains(searchTerm))
                    );
                }

                if (!string.IsNullOrWhiteSpace(sortBy))
                {
                    if (sortBy == "Sickness.Name")
                    {
                        query = sortAscending ? query.OrderBy(sp => sp.Sickness != null ? sp.Sickness.Name : "") : query.OrderByDescending(sp => sp.Sickness != null ? sp.Sickness.Name : "");
                    }
                    else if (sortBy == "Procedure.Name")
                    {
                        query = sortAscending ? query.OrderBy(sp => sp.Procedure != null ? sp.Procedure.Name : "") : query.OrderByDescending(sp => sp.Procedure != null ? sp.Procedure.Name : "");
                    }
                }
                else
                {
                    query = query.OrderBy(sp => sp.SicknessId).ThenBy(sp => sp.ProcedureId);
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting SicknessProcedure list");
                throw;
            }
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving SicknessProcedure");
                throw;
            }
        }

        public async Task AddAsync(SicknessProcedure entity, CancellationToken token = default)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                var exists = await _dbSet.AnyAsync(sp => sp.SicknessId == entity.SicknessId && sp.ProcedureId == entity.ProcedureId, token);
                if (exists)
                    throw new InvalidOperationException("This relationship already exists.");

                await _dbSet.AddAsync(entity, token);
                await _context.SaveChangesAsync(token);
                _logger.LogInformation("Added new SicknessProcedure");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding SicknessProcedure");
                throw;
            }
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
                _logger.LogInformation("Removed SicknessProcedure");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while removing SicknessProcedure");
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

        public async Task<IEnumerable<Procedure>> GetAllProceduresAsync(CancellationToken token = default)
        {
            try
            {
                return await _procedures.OrderBy(p => p.Name).ToListAsync(token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting Procedure list");
                throw;
            }
        }
    }
}

