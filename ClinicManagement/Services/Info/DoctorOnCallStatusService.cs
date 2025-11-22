using ClinicManagement.Data;
using ClinicManagement.Models.Info;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagement.Services.Info
{
    public interface IDoctorOnCallStatusService : IService<DoctorOnCallStatus>
    {
        Task<IEnumerable<Models.Humans.Doctor>> GetAllDoctorsAsync(CancellationToken token = default);
        Task<IEnumerable<Models.Info.Address>> GetAllAddressesAsync(CancellationToken token = default);
    }

    public class DoctorOnCallStatusService : Service<DoctorOnCallStatus>, IDoctorOnCallStatusService
    {
        private readonly DbSet<Models.Humans.Doctor> _doctors;
        private readonly DbSet<Models.Info.Address> _addresses;

        public DoctorOnCallStatusService(ClinicDbContext context, ILogger<DoctorOnCallStatusService> logger)
            : base(context, logger)
        {
            _doctors = _context.Set<Models.Humans.Doctor>();
            _addresses = _context.Set<Models.Info.Address>();
        }

        public override async Task<PaginatedResult<DoctorOnCallStatus>> GetAllAsync(
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
                var query = _dbSet.Include(d => d.Doctor).ThenInclude(d => d.Person).Include(d => d.Address).AsNoTracking();

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
                    query = ApplySorting(query, "StartTime", false);
                }

                var items = await query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync(token);

                var totalCount = await query.CountAsync(token);

                return new PaginatedResult<DoctorOnCallStatus>
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
                _logger.LogError(ex, "Error while getting DoctorOnCallStatus list");
                throw;
            }
        }

        public override async Task<DoctorOnCallStatus?> GetByIdAsync(int id, CancellationToken token = default)
        {
            try
            {
                return await _dbSet
                    .Include(d => d.Doctor).ThenInclude(d => d.Person)
                    .Include(d => d.Address)
                    .FirstOrDefaultAsync(d => d.Id == id, token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving DoctorOnCallStatus");
                throw;
            }
        }

        protected override IQueryable<DoctorOnCallStatus> ApplySearchFilter(IQueryable<DoctorOnCallStatus> query, string searchTerm)
        {
            return query.Where(d =>
                (d.Doctor != null && d.Doctor.Person != null && (
                    d.Doctor.Person.FirstName.Contains(searchTerm) ||
                    d.Doctor.Person.LastName.Contains(searchTerm) ||
                    (d.Doctor.Person.Patronymic != null && d.Doctor.Person.Patronymic.Contains(searchTerm))
                )) ||
                (d.Address != null && (
                    d.Address.Country.Contains(searchTerm) ||
                    d.Address.State.Contains(searchTerm) ||
                    d.Address.Locality.Contains(searchTerm) ||
                    d.Address.StreetName.Contains(searchTerm)
                ))
            );
        }

        protected override IQueryable<DoctorOnCallStatus> ApplySorting(IQueryable<DoctorOnCallStatus> query, string sortBy, bool ascending)
        {
            if (sortBy == "Doctor.Person.LastName")
            {
                query = ascending ? query.OrderBy(d => d.Doctor.Person.LastName) 
                    : query.OrderByDescending(d => d.Doctor.Person.LastName);
                return query;
            }
            if (sortBy == "Address.Locality")
            {
                query = ascending ? query.OrderBy(d => d.Address.Locality) 
                    : query.OrderByDescending(d => d.Address.Locality);
                return query;
            }
            if (sortBy == "Address.Country")
            {
                query = ascending ? query.OrderBy(d => d.Address.Country) 
                    : query.OrderByDescending(d => d.Address.Country);
                return query;
            }
            if (sortBy == "Address.State")
            {
                query = ascending ? query.OrderBy(d => d.Address.State) 
                    : query.OrderByDescending(d => d.Address.State);
                return query;
            }
            if (sortBy == "Address.StreetName")
            {
                query = ascending ? query.OrderBy(d => d.Address.StreetName) 
                    : query.OrderByDescending(d => d.Address.StreetName);
                return query;
            }
            return base.ApplySorting(query, sortBy, ascending);
        }

        public async Task<IEnumerable<Models.Humans.Doctor>> GetAllDoctorsAsync(CancellationToken token = default)
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

