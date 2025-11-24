using ClinicManagement.Data;
using ClinicManagement.Models.Humans;
using ClinicManagement.Models.Info;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagement.Services.Humans
{
    public interface IPatientService : IService<Patient>
    {
        Task<IEnumerable<Person>> GetAllPeopleAsync(CancellationToken token = default);
        Task<IEnumerable<Address>> GetAllAddressesAsync(CancellationToken token = default);
    }

    public class PatientService : Service<Patient>, IPatientService
    {
        private readonly DbSet<Person> _people;
        private readonly DbSet<Address> _addresses;

        public PatientService(ClinicDbContext context, ILogger<PatientService> logger)
            : base(context, logger)
        {
            _people = _context.Set<Person>();
            _addresses = _context.Set<Address>();
        }

        public override async Task<PaginatedResult<Patient>> GetAllAsync(
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
                var query = _dbSet.Include(p => p.Person).Include(p => p.Address).AsNoTracking();

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

                return new PaginatedResult<Patient>
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
                _logger.LogWarning("GetAllAsync operation for {Entity} was canceled", typeof(Patient).Name);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting {Entity} list", typeof(Patient).Name);
                throw;
            }
        }

        public override async Task<Patient?> GetByIdAsync(int id, CancellationToken token = default)
        {
            try
            {
                return await _dbSet
                    .Include(p => p.Person)
                    .Include(p => p.Address)
                    .FirstOrDefaultAsync(p => p.Id == id, token);
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning("GetByIdAsync for {Entity} with id {Id} was canceled", typeof(Patient).Name, id);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving {Entity} with id {Id}", typeof(Patient).Name, id);
                throw;
            }
        }

        protected override IQueryable<Patient> ApplySearchFilter(IQueryable<Patient> query, string searchTerm)
        {
            return query.Where(p =>
                (p.Person != null && (
                    p.Person.FirstName.Contains(searchTerm) ||
                    p.Person.LastName.Contains(searchTerm) ||
                    (p.Person.Patronymic != null && p.Person.Patronymic.Contains(searchTerm))
                )) ||
                (p.Address != null && (
                    p.Address.Country.Contains(searchTerm) ||
                    p.Address.State.Contains(searchTerm) ||
                    p.Address.Locality.Contains(searchTerm) ||
                    p.Address.StreetName.Contains(searchTerm)
                ))
            );
        }

        protected override IQueryable<Patient> ApplySorting(IQueryable<Patient> query, string sortBy, bool ascending)
        {
            if (sortBy == "Person.LastName")
            {
                query = ascending ? query.OrderBy(p => p.Person != null ? p.Person.LastName : "") 
                    : query.OrderByDescending(p => p.Person != null ? p.Person.LastName : "");
                return query;
            }
            if (sortBy == "Person.FirstName")
            {
                query = ascending ? query.OrderBy(p => p.Person != null ? p.Person.FirstName : "") 
                    : query.OrderByDescending(p => p.Person != null ? p.Person.FirstName : "");
                return query;
            }
            if (sortBy == "Address.Locality")
            {
                query = ascending ? query.OrderBy(p => p.Address != null ? p.Address.Locality : "") 
                    : query.OrderByDescending(p => p.Address != null ? p.Address.Locality : "");
                return query;
            }
            if (sortBy == "Address.Country")
            {
                query = ascending ? query.OrderBy(p => p.Address != null ? p.Address.Country : "") 
                    : query.OrderByDescending(p => p.Address != null ? p.Address.Country : "");
                return query;
            }
            if (sortBy == "Address.State")
            {
                query = ascending ? query.OrderBy(p => p.Address != null ? p.Address.State : "") 
                    : query.OrderByDescending(p => p.Address != null ? p.Address.State : "");
                return query;
            }
            if (sortBy == "Address.StreetName")
            {
                query = ascending ? query.OrderBy(p => p.Address != null ? p.Address.StreetName : "") 
                    : query.OrderByDescending(p => p.Address != null ? p.Address.StreetName : "");
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
            catch (OperationCanceledException)
            {
                _logger.LogWarning("GetAllPeopleAsync operation was canceled");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting Person list");
                throw;
            }
        }

        public async Task<IEnumerable<Address>> GetAllAddressesAsync(CancellationToken token = default)
        {
            try
            {
                return await _addresses
                    .OrderBy(a => a.Country)
                    .ThenBy(a => a.State)
                    .ThenBy(a => a.Locality)
                    .ToListAsync(token);
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning("GetAllAddressesAsync operation was canceled");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting Address list");
                throw;
            }
        }
    }
}

