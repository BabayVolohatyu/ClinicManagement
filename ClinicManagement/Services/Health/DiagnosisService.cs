using ClinicManagement.Data;
using ClinicManagement.Models.Health;
using ClinicManagement.Models.Info;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagement.Services.Health
{
    public interface IDiagnosisService : IService<Diagnosis>
    {
        Task<IEnumerable<Appointment>> GetAllAppointmentsAsync(CancellationToken token = default);
    }

    public class DiagnosisService : Service<Diagnosis>, IDiagnosisService
    {
        private readonly DbSet<Appointment> _appointments;

        public DiagnosisService(ClinicDbContext context, ILogger<DiagnosisService> logger)
            : base(context, logger)
        {
            _appointments = _context.Set<Appointment>();
        }

        public override async Task<PaginatedResult<Diagnosis>> GetAllAsync(
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
                var query = _dbSet.Include(d => d.Appointment)
                    .ThenInclude(a => a.Patient).ThenInclude(p => p.Person)
                    .Include(d => d.Appointment)
                    .ThenInclude(a => a.DoctorProcedure).ThenInclude(dp => dp.Doctor).ThenInclude(d => d.Person)
                    .AsNoTracking();

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

                return new PaginatedResult<Diagnosis>
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
                _logger.LogError(ex, "Error while getting Diagnosis list");
                throw;
            }
        }

        public override async Task<Diagnosis?> GetByIdAsync(int id, CancellationToken token = default)
        {
            try
            {
                return await _dbSet
                    .Include(d => d.Appointment)
                    .ThenInclude(a => a.Patient).ThenInclude(p => p.Person)
                    .Include(d => d.Appointment)
                    .ThenInclude(a => a.DoctorProcedure).ThenInclude(dp => dp.Doctor).ThenInclude(d => d.Person)
                    .Include(d => d.Appointment)
                    .ThenInclude(a => a.Cabinet)
                    .FirstOrDefaultAsync(d => d.Id == id, token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving Diagnosis");
                throw;
            }
        }

        protected override IQueryable<Diagnosis> ApplySearchFilter(IQueryable<Diagnosis> query, string searchTerm)
        {
            return query.Where(d =>
                d.Prescription.Contains(searchTerm) ||
                (d.Appointment != null && d.Appointment.Patient != null && d.Appointment.Patient.Person != null && (
                    d.Appointment.Patient.Person.FirstName.Contains(searchTerm) ||
                    d.Appointment.Patient.Person.LastName.Contains(searchTerm) ||
                    (d.Appointment.Patient.Person.Patronymic != null && d.Appointment.Patient.Person.Patronymic.Contains(searchTerm))
                )) ||
                (d.Appointment != null && d.Appointment.DoctorProcedure != null && d.Appointment.DoctorProcedure.Doctor != null && d.Appointment.DoctorProcedure.Doctor.Person != null && (
                    d.Appointment.DoctorProcedure.Doctor.Person.FirstName.Contains(searchTerm) ||
                    d.Appointment.DoctorProcedure.Doctor.Person.LastName.Contains(searchTerm)
                ))
            );
        }

        protected override IQueryable<Diagnosis> ApplySorting(IQueryable<Diagnosis> query, string sortBy, bool ascending)
        {
            if (sortBy == "Appointment.Patient.Person.LastName")
            {
                query = ascending ? query.OrderBy(d => d.Appointment.Patient.Person.LastName) 
                    : query.OrderByDescending(d => d.Appointment.Patient.Person.LastName);
                return query;
            }
            if (sortBy == "Appointment.DoctorProcedure.Doctor.Person.LastName")
            {
                query = ascending ? query.OrderBy(d => d.Appointment.DoctorProcedure.Doctor.Person.LastName) 
                    : query.OrderByDescending(d => d.Appointment.DoctorProcedure.Doctor.Person.LastName);
                return query;
            }
            return base.ApplySorting(query, sortBy, ascending);
        }

        public async Task<IEnumerable<Appointment>> GetAllAppointmentsAsync(CancellationToken token = default)
        {
            try
            {
                return await _appointments
                    .Include(a => a.Patient).ThenInclude(p => p.Person)
                    .Include(a => a.DoctorProcedure).ThenInclude(dp => dp.Doctor).ThenInclude(d => d.Person)
                    .Where(a => a.Diagnosis == null) // Only show appointments without diagnosis
                    .OrderByDescending(a => a.StartTime)
                    .ToListAsync(token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting Appointment list");
                throw;
            }
        }
    }
}

