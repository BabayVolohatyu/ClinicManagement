using ClinicManagement.Data;
using ClinicManagement.Models.Humans;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagement.Services.Humans
{
    public interface IPatientService : IService<Patient>
    {
        Task<IEnumerable<Person>> GetAllPeopleAsync(CancellationToken token = default);
        Task<IEnumerable<Models.Info.Address>> GetAllAddressesAsync(CancellationToken token = default);
    }

    public class PatientService : Service<Patient>, IPatientService
    {
        private readonly DbSet<Person> _people;
        private readonly DbSet<Models.Info.Address> _addresses;

        public PatientService(ClinicDbContext context, ILogger<PatientService> logger)
            : base(context, logger)
        {
            _people = _context.Set<Person>();
            _addresses = _context.Set<Models.Info.Address>();
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

        public async Task<IEnumerable<Models.Info.Address>> GetAllAddressesAsync(CancellationToken token = default)
        {
            try
            {
                return await _addresses
                    .OrderBy(a => a.Country)
                    .ThenBy(a => a.State)
                    .ThenBy(a => a.Locality)
                    .ToListAsync(token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting Address list");
                throw;
            }
        }
    }
}

