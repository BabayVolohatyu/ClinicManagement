using ClinicManagement.Data;
using ClinicManagement.Models.Humans;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagement.Services.Humans
{
    public interface IDistrictDoctorService
    {
        Task AddAsync(DistrictDoctor entity, CancellationToken token = default);
        Task RemoveAsync(int doctorId, CancellationToken token = default);
        Task<DistrictDoctor?> GetByIdAsync(int doctorId, CancellationToken token = default);
        Task<PaginatedResult<DistrictDoctor>> GetAllAsync(
            int pageNumber = 1,
            int pageSize = 10,
            string? searchTerm = null,
            string? sortBy = null,
            bool sortAscending = true,
            CancellationToken token = default);
        Task<IEnumerable<Doctor>> GetAllDoctorsAsync(CancellationToken token = default);
    }

    public class DistrictDoctorService : IDistrictDoctorService
    {
        protected readonly ClinicDbContext _context;
        protected readonly DbSet<DistrictDoctor> _dbSet;
        protected readonly ILogger<DistrictDoctorService> _logger;
        private readonly DbSet<Doctor> _doctors;

        public DistrictDoctorService(ClinicDbContext context, ILogger<DistrictDoctorService> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<DistrictDoctor>();
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _doctors = _context.Set<Doctor>();
        }

        public async Task<PaginatedResult<DistrictDoctor>> GetAllAsync(
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
                var query = _dbSet.Include(dd => dd.Doctor).ThenInclude(d => d.Person).Include(dd => dd.Doctor).ThenInclude(d => d.Specialty).AsNoTracking();

                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    query = query.Where(dd =>
                        (dd.Doctor != null && dd.Doctor.Person != null && (
                            dd.Doctor.Person.FirstName.Contains(searchTerm) ||
                            dd.Doctor.Person.LastName.Contains(searchTerm) ||
                            (dd.Doctor.Person.Patronymic != null && dd.Doctor.Person.Patronymic.Contains(searchTerm))
                        )) ||
                        (dd.Doctor != null && dd.Doctor.Specialty != null && dd.Doctor.Specialty.Name.Contains(searchTerm))
                    );
                }

                query = query.OrderBy(dd => dd.DoctorId);

                var items = await query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync(token);

                var totalCount = await query.CountAsync(token);

                return new PaginatedResult<DistrictDoctor>
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
                _logger.LogError(ex, "Error while getting DistrictDoctor list");
                throw;
            }
        }

        public async Task<DistrictDoctor?> GetByIdAsync(int doctorId, CancellationToken token = default)
        {
            try
            {
                return await _dbSet
                    .Include(dd => dd.Doctor).ThenInclude(d => d.Person)
                    .Include(dd => dd.Doctor).ThenInclude(d => d.Specialty)
                    .FirstOrDefaultAsync(dd => dd.DoctorId == doctorId, token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving DistrictDoctor");
                throw;
            }
        }

        public async Task AddAsync(DistrictDoctor entity, CancellationToken token = default)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                var exists = await _dbSet.AnyAsync(dd => dd.DoctorId == entity.DoctorId, token);
                if (exists)
                    throw new InvalidOperationException("This doctor is already a district doctor.");

                await _dbSet.AddAsync(entity, token);
                await _context.SaveChangesAsync(token);
                _logger.LogInformation("Added new DistrictDoctor");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding DistrictDoctor");
                throw;
            }
        }

        public async Task RemoveAsync(int doctorId, CancellationToken token = default)
        {
            try
            {
                var entity = await GetByIdAsync(doctorId, token);
                if (entity == null)
                    throw new KeyNotFoundException($"DistrictDoctor with DoctorId {doctorId} not found");

                _dbSet.Remove(entity);
                await _context.SaveChangesAsync(token);
                _logger.LogInformation("Removed DistrictDoctor");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while removing DistrictDoctor");
                throw;
            }
        }

        public async Task<IEnumerable<Doctor>> GetAllDoctorsAsync(CancellationToken token = default)
        {
            try
            {
                return await _doctors
                    .Include(d => d.Person)
                    .Include(d => d.Specialty)
                    .OrderBy(d => d.Person != null ? d.Person.LastName : "")
                    .ThenBy(d => d.Person != null ? d.Person.FirstName : "")
                    .ToListAsync(token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting Doctor list");
                throw;
            }
        }
    }
}

