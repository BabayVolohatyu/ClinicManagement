using ClinicManagement.Data;
using ClinicManagement.Models.Humans;
using ClinicManagement.Models.Info;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagement.Services.Info
{
    public interface IHomeCallLogService : IService<HomeCallLog>
    {
        Task<IEnumerable<Doctor>> GetAllDoctorsAsync(CancellationToken token = default);
        Task<IEnumerable<Address>> GetAllAddressesAsync(CancellationToken token = default);
    }

    public class HomeCallLogService : Service<HomeCallLog>, IHomeCallLogService
    {
        private readonly DbSet<Doctor> _doctors;
        private readonly DbSet<Address> _addresses;

        public HomeCallLogService(ClinicDbContext context, ILogger<HomeCallLogService> logger)
            : base(context, logger)
        {
            _doctors = _context.Set<Doctor>();
            _addresses = _context.Set<Address>();
        }

        public override async Task<PaginatedResult<HomeCallLog>> GetAllAsync(
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
                var query = _dbSet.Include(h => h.Doctor).ThenInclude(d => d.Person).Include(h => h.Address).AsNoTracking();

                
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
                    
                    query = ApplySorting(query, "DateTime", false);
                }

                var items = await query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync(token);

                var totalCount = await query.CountAsync(token);

                return new PaginatedResult<HomeCallLog>
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
                _logger.LogWarning("GetAllAsync operation for {Entity} was canceled", typeof(HomeCallLog).Name);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting {Entity} list", typeof(HomeCallLog).Name);
                throw;
            }
        }

        public override async Task<HomeCallLog?> GetByIdAsync(int id, CancellationToken token = default)
        {
            try
            {
                return await _dbSet
                    .Include(h => h.Doctor).ThenInclude(d => d.Person)
                    .Include(h => h.Address)
                    .FirstOrDefaultAsync(h => h.Id == id, token);
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning("GetByIdAsync for {Entity} with id {Id} was canceled", typeof(HomeCallLog).Name, id);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving {Entity} with id {Id}", typeof(HomeCallLog).Name, id);
                throw;
            }
        }

        protected override IQueryable<HomeCallLog> ApplySearchFilter(IQueryable<HomeCallLog> query, string searchTerm)
        {
            return query.Where(h =>
                (h.Doctor != null && h.Doctor.Person != null && (
                    h.Doctor.Person.FirstName.Contains(searchTerm) ||
                    h.Doctor.Person.LastName.Contains(searchTerm) ||
                    (h.Doctor.Person.Patronymic != null && h.Doctor.Person.Patronymic.Contains(searchTerm))
                )) ||
                (h.Address != null && (
                    h.Address.Country.Contains(searchTerm) ||
                    h.Address.State.Contains(searchTerm) ||
                    h.Address.Locality.Contains(searchTerm) ||
                    h.Address.StreetName.Contains(searchTerm)
                ))
            );
        }

        protected override IQueryable<HomeCallLog> ApplySorting(IQueryable<HomeCallLog> query, string sortBy, bool ascending)
        {
            if (sortBy == "Doctor.Person.LastName")
            {
                query = ascending ? query.OrderBy(h => h.Doctor.Person.LastName) 
                    : query.OrderByDescending(h => h.Doctor.Person.LastName);
                return query;
            }
            if (sortBy == "Address.Locality")
            {
                query = ascending ? query.OrderBy(h => h.Address.Locality) 
                    : query.OrderByDescending(h => h.Address.Locality);
                return query;
            }
            if (sortBy == "Address.Country")
            {
                query = ascending ? query.OrderBy(h => h.Address.Country) 
                    : query.OrderByDescending(h => h.Address.Country);
                return query;
            }
            if (sortBy == "Address.State")
            {
                query = ascending ? query.OrderBy(h => h.Address.State) 
                    : query.OrderByDescending(h => h.Address.State);
                return query;
            }
            if (sortBy == "Address.StreetName")
            {
                query = ascending ? query.OrderBy(h => h.Address.StreetName) 
                    : query.OrderByDescending(h => h.Address.StreetName);
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

