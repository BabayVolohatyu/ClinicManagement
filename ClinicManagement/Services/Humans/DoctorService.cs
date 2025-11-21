using ClinicManagement.Data;
using ClinicManagement.Models.Humans;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagement.Services.Humans
{
    public interface IDoctorService : IService<Doctor>
    {
        Task<IEnumerable<Person>> GetAllPeopleAsync(CancellationToken token = default);
        Task<IEnumerable<Specialty>> GetAllSpecialtiesAsync(CancellationToken token = default);
    }

    public class DoctorService : Service<Doctor>, IDoctorService
    {
        private readonly DbSet<Person> _people;
        private readonly DbSet<Specialty> _specialties;

        public DoctorService(ClinicDbContext context, ILogger<DoctorService> logger)
            : base(context, logger)
        {
            _people = _context.Set<Person>();
            _specialties = _context.Set<Specialty>();
        }

        public override async Task<PaginatedResult<Doctor>> GetAllAsync(
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
                var query = _dbSet.Include(d => d.Person).Include(d => d.Specialty).AsNoTracking();

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
                    query = ApplySorting(query, "Id", true);
                }

                var items = await query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync(token);

                var totalCount = await query.CountAsync(token);

                return new PaginatedResult<Doctor>
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
                _logger.LogWarning("GetAllAsync operation for {Entity} was canceled", typeof(Doctor).Name);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting {Entity} list", typeof(Doctor).Name);
                throw;
            }
        }

        public override async Task<Doctor?> GetByIdAsync(int id, CancellationToken token = default)
        {
            try
            {
                return await _dbSet
                    .Include(d => d.Person)
                    .Include(d => d.Specialty)
                    .FirstOrDefaultAsync(d => d.Id == id, token);
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning("GetByIdAsync for {Entity} with id {Id} was canceled", typeof(Doctor).Name, id);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving {Entity} with id {Id}", typeof(Doctor).Name, id);
                throw;
            }
        }

        protected override IQueryable<Doctor> ApplySearchFilter(IQueryable<Doctor> query, string searchTerm)
        {
            return query.Where(d =>
                (d.Person != null && (
                    d.Person.FirstName.Contains(searchTerm) ||
                    d.Person.LastName.Contains(searchTerm) ||
                    (d.Person.Patronymic != null && d.Person.Patronymic.Contains(searchTerm))
                )) ||
                (d.Specialty != null && d.Specialty.Name.Contains(searchTerm))
            );
        }

        protected override IQueryable<Doctor> ApplySorting(IQueryable<Doctor> query, string sortBy, bool ascending)
        {
            if (sortBy == "Person.LastName" || sortBy == "Person.FirstName")
            {
                if (sortBy == "Person.LastName")
                {
                    query = ascending ? query.OrderBy(d => d.Person != null ? d.Person.LastName : "") : query.OrderByDescending(d => d.Person != null ? d.Person.LastName : "");
                }
                else if (sortBy == "Person.FirstName")
                {
                    query = ascending ? query.OrderBy(d => d.Person != null ? d.Person.FirstName : "") : query.OrderByDescending(d => d.Person != null ? d.Person.FirstName : "");
                }
                return query;
            }
            if (sortBy == "Specialty.Name")
            {
                query = ascending ? query.OrderBy(d => d.Specialty != null ? d.Specialty.Name : "") : query.OrderByDescending(d => d.Specialty != null ? d.Specialty.Name : "");
                return query;
            }
            return base.ApplySorting(query, sortBy, ascending);
        }

        public async Task<IEnumerable<Person>> GetAllPeopleAsync(CancellationToken token = default)
        {
            try
            {
                return await _people
                    .OrderBy(p => p.LastName)
                    .ThenBy(p => p.FirstName)
                    .ToListAsync(token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting Person list");
                throw;
            }
        }

        public async Task<IEnumerable<Specialty>> GetAllSpecialtiesAsync(CancellationToken token = default)
        {
            try
            {
                return await _specialties
                    .OrderBy(s => s.Name)
                    .ToListAsync(token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting Specialty list");
                throw;
            }
        }
    }
}

