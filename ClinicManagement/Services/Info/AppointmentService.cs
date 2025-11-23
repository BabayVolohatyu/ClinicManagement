using ClinicManagement.Data;
using ClinicManagement.Models.Info;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagement.Services.Info
{
    public interface IAppointmentService : IService<Appointment>
    {
        Task<IEnumerable<DoctorProcedure>> GetAllDoctorProceduresAsync(CancellationToken token = default);
        Task<IEnumerable<Models.Facilities.Cabinet>> GetAllCabinetsAsync(CancellationToken token = default);
        Task<IEnumerable<Models.Humans.Patient>> GetAllPatientsAsync(CancellationToken token = default);
    }

    public class AppointmentService : Service<Appointment>, IAppointmentService
    {
        private readonly DbSet<DoctorProcedure> _doctorProcedures;
        private readonly DbSet<Models.Facilities.Cabinet> _cabinets;
        private readonly DbSet<Models.Humans.Patient> _patients;

        public AppointmentService(ClinicDbContext context, ILogger<AppointmentService> logger)
            : base(context, logger)
        {
            _doctorProcedures = _context.Set<DoctorProcedure>();
            _cabinets = _context.Set<Models.Facilities.Cabinet>();
            _patients = _context.Set<Models.Humans.Patient>();
        }

        public override async Task<PaginatedResult<Appointment>> GetAllAsync(
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
                var query = _dbSet
                    .Include(a => a.DoctorProcedure).ThenInclude(dp => dp.Doctor).ThenInclude(d => d.Person)
                    .Include(a => a.DoctorProcedure).ThenInclude(dp => dp.Procedure)
                    .Include(a => a.Cabinet).ThenInclude(c => c.Type)
                    .Include(a => a.Patient).ThenInclude(p => p.Person)
                    .AsNoTracking();

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
                    // Default sorting by StartTime if no sort specified
                    query = ApplySorting(query, "StartTime", false);
                }

                var items = await query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync(token);

                var totalCount = await query.CountAsync(token);

                return new PaginatedResult<Appointment>
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
                _logger.LogWarning("GetAllAsync operation for {Entity} was canceled", typeof(Appointment).Name);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting {Entity} list", typeof(Appointment).Name);
                throw;
            }
        }

        public override async Task<Appointment?> GetByIdAsync(int id, CancellationToken token = default)
        {
            try
            {
                return await _dbSet
                    .Include(a => a.DoctorProcedure).ThenInclude(dp => dp.Doctor).ThenInclude(d => d.Person)
                    .Include(a => a.DoctorProcedure).ThenInclude(dp => dp.Procedure)
                    .Include(a => a.Cabinet).ThenInclude(c => c.Type)
                    .Include(a => a.Patient).ThenInclude(p => p.Person)
                    .Include(a => a.Diagnosis)
                    .FirstOrDefaultAsync(a => a.Id == id, token);
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning("GetByIdAsync for {Entity} with id {Id} was canceled", typeof(Appointment).Name, id);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving {Entity} with id {Id}", typeof(Appointment).Name, id);
                throw;
            }
        }

        protected override IQueryable<Appointment> ApplySearchFilter(IQueryable<Appointment> query, string searchTerm)
        {
            return query.Where(a =>
                (a.DoctorProcedure != null && a.DoctorProcedure.Doctor != null && a.DoctorProcedure.Doctor.Person != null && (
                    a.DoctorProcedure.Doctor.Person.FirstName.Contains(searchTerm) ||
                    a.DoctorProcedure.Doctor.Person.LastName.Contains(searchTerm) ||
                    (a.DoctorProcedure.Doctor.Person.Patronymic != null && a.DoctorProcedure.Doctor.Person.Patronymic.Contains(searchTerm))
                )) ||
                (a.Patient != null && a.Patient.Person != null && (
                    a.Patient.Person.FirstName.Contains(searchTerm) ||
                    a.Patient.Person.LastName.Contains(searchTerm) ||
                    (a.Patient.Person.Patronymic != null && a.Patient.Person.Patronymic.Contains(searchTerm))
                )) ||
                (a.Cabinet != null && (
                    a.Cabinet.Building.ToString().Contains(searchTerm) ||
                    a.Cabinet.Floor.ToString().Contains(searchTerm) ||
                    a.Cabinet.Number.ToString().Contains(searchTerm)
                ))
            );
        }

        protected override IQueryable<Appointment> ApplySorting(IQueryable<Appointment> query, string sortBy, bool ascending)
        {
            if (sortBy == "DoctorProcedure.Doctor.Person.LastName")
            {
                query = ascending ? query.OrderBy(a => a.DoctorProcedure.Doctor.Person.LastName) 
                    : query.OrderByDescending(a => a.DoctorProcedure.Doctor.Person.LastName);
                return query;
            }
            if (sortBy == "Patient.Person.LastName")
            {
                query = ascending ? query.OrderBy(a => a.Patient.Person.LastName) 
                    : query.OrderByDescending(a => a.Patient.Person.LastName);
                return query;
            }
            if (sortBy == "Cabinet.Building")
            {
                query = ascending ? query.OrderBy(a => a.Cabinet.Building) 
                    : query.OrderByDescending(a => a.Cabinet.Building);
                return query;
            }
            if (sortBy == "Cabinet.Floor")
            {
                query = ascending ? query.OrderBy(a => a.Cabinet.Floor) 
                    : query.OrderByDescending(a => a.Cabinet.Floor);
                return query;
            }
            if (sortBy == "Cabinet.Number")
            {
                query = ascending ? query.OrderBy(a => a.Cabinet.Number) 
                    : query.OrderByDescending(a => a.Cabinet.Number);
                return query;
            }
            return base.ApplySorting(query, sortBy, ascending);
        }

        public async Task<IEnumerable<DoctorProcedure>> GetAllDoctorProceduresAsync(CancellationToken token = default)
        {
            try
            {
                return await _doctorProcedures
                    .Include(dp => dp.Doctor).ThenInclude(d => d.Person)
                    .Include(dp => dp.Procedure)
                    .OrderBy(dp => dp.Doctor != null && dp.Doctor.Person != null ? dp.Doctor.Person.LastName : "")
                    .ThenBy(dp => dp.Doctor != null && dp.Doctor.Person != null ? dp.Doctor.Person.FirstName : "")
                    .ThenBy(dp => dp.Procedure != null ? dp.Procedure.Name : "")
                    .ToListAsync(token);
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning("GetAllDoctorProceduresAsync operation was canceled");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting DoctorProcedure list");
                throw;
            }
        }

        public async Task<IEnumerable<Models.Facilities.Cabinet>> GetAllCabinetsAsync(CancellationToken token = default)
        {
            try
            {
                return await _cabinets
                    .Include(c => c.Type)
                    .OrderBy(c => c.Building)
                    .ThenBy(c => c.Floor)
                    .ThenBy(c => c.Number)
                    .ToListAsync(token);
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning("GetAllCabinetsAsync operation was canceled");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting Cabinet list");
                throw;
            }
        }

        public async Task<IEnumerable<Models.Humans.Patient>> GetAllPatientsAsync(CancellationToken token = default)
        {
            try
            {
                return await _patients
                    .Include(p => p.Person)
                    .OrderBy(p => p.Person != null ? p.Person.LastName : "")
                    .ThenBy(p => p.Person != null ? p.Person.FirstName : "")
                    .ToListAsync(token);
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning("GetAllPatientsAsync operation was canceled");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting Patient list");
                throw;
            }
        }
    }
}

