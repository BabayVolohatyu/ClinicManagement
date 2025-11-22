using ClinicManagement.Data;
using ClinicManagement.Models.Humans;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagement.Services.Humans
{
    public interface IDistrictDoctorService : IService<DistrictDoctor>
    {
        Task<IEnumerable<Doctor>> GetAllDoctorsAsync(CancellationToken token = default);
    }

    public class DistrictDoctorService : Service<DistrictDoctor>, IDistrictDoctorService
    {
        private readonly DbSet<Doctor> _doctors;

        public DistrictDoctorService(ClinicDbContext context, ILogger<DistrictDoctorService> logger)
            : base(context, logger)
        {
            _doctors = _context.Set<Doctor>();
        }

        public override async Task<PaginatedResult<DistrictDoctor>> GetAllAsync(
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
                    query = ApplySearchFilter(query, searchTerm);
                }

                if (!string.IsNullOrWhiteSpace(sortBy))
                {
                    query = ApplySorting(query, sortBy, sortAscending);
                }
                else
                {
                    query = ApplySorting(query, "DoctorId", true);
                }

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
            catch (OperationCanceledException)
            {
                _logger.LogWarning("GetAllAsync operation for {Entity} was canceled", typeof(DistrictDoctor).Name);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting {Entity} list", typeof(DistrictDoctor).Name);
                throw;
            }
        }

        public override async Task<DistrictDoctor?> GetByIdAsync(int id, CancellationToken token = default)
        {
            try
            {
                return await _dbSet
                    .Include(dd => dd.Doctor).ThenInclude(d => d.Person)
                    .Include(dd => dd.Doctor).ThenInclude(d => d.Specialty)
                    .FirstOrDefaultAsync(dd => dd.DoctorId == id, token);
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning("GetByIdAsync for {Entity} with id {Id} was canceled", typeof(DistrictDoctor).Name, id);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving {Entity} with id {Id}", typeof(DistrictDoctor).Name, id);
                throw;
            }
        }

        public override async Task AddAsync(DistrictDoctor entity, CancellationToken token = default)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                var exists = await _dbSet.AnyAsync(dd => dd.DoctorId == entity.DoctorId, token);
                if (exists)
                    throw new InvalidOperationException("This doctor is already a district doctor.");

                await base.AddAsync(entity, token);
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning("AddAsync for {Entity} was canceled", typeof(DistrictDoctor).Name);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding {Entity}", typeof(DistrictDoctor).Name);
                throw;
            }
        }

        public override async Task RemoveAsync(int id, CancellationToken token = default)
        {
            try
            {
                var entity = await GetByIdAsync(id, token);
                if (entity == null)
                    throw new KeyNotFoundException($"DistrictDoctor with DoctorId {id} not found");

                _dbSet.Remove(entity);
                await _context.SaveChangesAsync(token);
                _logger.LogInformation("Removed {Entity} with id {Id}", typeof(DistrictDoctor).Name, id);
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning("RemoveAsync for {Entity} with id {Id} was canceled", typeof(DistrictDoctor).Name, id);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while removing {Entity} with id {Id}", typeof(DistrictDoctor).Name, id);
                throw;
            }
        }

        protected override IQueryable<DistrictDoctor> ApplySearchFilter(IQueryable<DistrictDoctor> query, string searchTerm)
        {
            return query.Where(dd =>
                (dd.Doctor != null && dd.Doctor.Person != null && (
                    dd.Doctor.Person.FirstName.Contains(searchTerm) ||
                    dd.Doctor.Person.LastName.Contains(searchTerm) ||
                    (dd.Doctor.Person.Patronymic != null && dd.Doctor.Person.Patronymic.Contains(searchTerm))
                )) ||
                (dd.Doctor != null && dd.Doctor.Specialty != null && dd.Doctor.Specialty.Name.Contains(searchTerm))
            );
        }

        protected override IQueryable<DistrictDoctor> ApplySorting(IQueryable<DistrictDoctor> query, string sortBy, bool ascending)
        {
            if (sortBy == "Doctor.Person.LastName")
            {
                query = ascending ? query.OrderBy(dd => dd.Doctor != null && dd.Doctor.Person != null ? dd.Doctor.Person.LastName : "") 
                    : query.OrderByDescending(dd => dd.Doctor != null && dd.Doctor.Person != null ? dd.Doctor.Person.LastName : "");
                return query;
            }
            if (sortBy == "Doctor.Specialty.Name")
            {
                query = ascending ? query.OrderBy(dd => dd.Doctor != null && dd.Doctor.Specialty != null ? dd.Doctor.Specialty.Name : "") 
                    : query.OrderByDescending(dd => dd.Doctor != null && dd.Doctor.Specialty != null ? dd.Doctor.Specialty.Name : "");
                return query;
            }
            return base.ApplySorting(query, sortBy, ascending);
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
            catch (OperationCanceledException)
            {
                _logger.LogWarning("GetAllDoctorsAsync operation was canceled");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting Doctor list");
                throw;
            }
        }
    }
}

