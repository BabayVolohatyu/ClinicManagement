using ClinicManagement.Data;
using ClinicManagement.Models.Health;
using ClinicManagement.Models.Humans;
using ClinicManagement.Models.Info;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagement.Services.Info
{
    public interface IDoctorProcedureService : IService<DoctorProcedure>
    {
        Task<IEnumerable<Doctor>> GetAllDoctorsAsync(CancellationToken token = default);
        Task<IEnumerable<Procedure>> GetAllProceduresAsync(CancellationToken token = default);
    }

    public class DoctorProcedureService : Service<DoctorProcedure>, IDoctorProcedureService
    {
        private readonly DbSet<Doctor> _doctors;
        private readonly DbSet<Procedure> _procedures;

        public DoctorProcedureService(ClinicDbContext context, ILogger<DoctorProcedureService> logger)
            : base(context, logger)
        {
            _doctors = _context.Set<Doctor>();
            _procedures = _context.Set<Procedure>();
        }

        public override async Task<PaginatedResult<DoctorProcedure>> GetAllAsync(
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
                var query = _dbSet.Include(dp => dp.Doctor).ThenInclude(d => d.Person).Include(dp => dp.Procedure).AsNoTracking();

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

                return new PaginatedResult<DoctorProcedure>
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
                _logger.LogError(ex, "Error while getting DoctorProcedure list");
                throw;
            }
        }

        public override async Task<DoctorProcedure?> GetByIdAsync(int id, CancellationToken token = default)
        {
            try
            {
                return await _dbSet
                    .Include(dp => dp.Doctor).ThenInclude(d => d.Person)
                    .Include(dp => dp.Procedure)
                    .FirstOrDefaultAsync(dp => dp.Id == id, token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving DoctorProcedure");
                throw;
            }
        }

        protected override IQueryable<DoctorProcedure> ApplySearchFilter(IQueryable<DoctorProcedure> query, string searchTerm)
        {
            return query.Where(dp =>
                (dp.Doctor != null && dp.Doctor.Person != null && (
                    dp.Doctor.Person.FirstName.Contains(searchTerm) ||
                    dp.Doctor.Person.LastName.Contains(searchTerm) ||
                    (dp.Doctor.Person.Patronymic != null && dp.Doctor.Person.Patronymic.Contains(searchTerm))
                )) ||
                (dp.Procedure != null && (
                    dp.Procedure.Name.Contains(searchTerm) ||
                    (dp.Procedure.Description != null && dp.Procedure.Description.Contains(searchTerm))
                ))
            );
        }

        protected override IQueryable<DoctorProcedure> ApplySorting(IQueryable<DoctorProcedure> query, string sortBy, bool ascending)
        {
            if (sortBy == "Doctor.Person.LastName")
            {
                query = ascending ? query.OrderBy(dp => dp.Doctor.Person.LastName) 
                    : query.OrderByDescending(dp => dp.Doctor.Person.LastName);
                return query;
            }
            if (sortBy == "Procedure.Name")
            {
                query = ascending ? query.OrderBy(dp => dp.Procedure.Name) 
                    : query.OrderByDescending(dp => dp.Procedure.Name);
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting Doctor list");
                throw;
            }
        }

        public async Task<IEnumerable<Procedure>> GetAllProceduresAsync(CancellationToken token = default)
        {
            try
            {
                return await _procedures
                    .OrderBy(p => p.Name)
                    .ToListAsync(token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting Procedure list");
                throw;
            }
        }
    }
}

