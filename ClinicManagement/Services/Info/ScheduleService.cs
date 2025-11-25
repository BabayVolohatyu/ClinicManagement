using ClinicManagement.Data;
using ClinicManagement.Models.Facilities;
using ClinicManagement.Models.Humans;
using ClinicManagement.Models.Info;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagement.Services.Info
{
    public interface IScheduleService : IService<Schedule>
    {
        Task<IEnumerable<Doctor>> GetAllDoctorsAsync(CancellationToken token = default);
        Task<IEnumerable<Cabinet>> GetAllCabinetsAsync(CancellationToken token = default);
    }

    public class ScheduleService : Service<Schedule>, IScheduleService
    {
        private readonly DbSet<Doctor> _doctors;
        private readonly DbSet<Cabinet> _cabinets;

        public ScheduleService(ClinicDbContext context, ILogger<ScheduleService> logger)
            : base(context, logger)
        {
            _doctors = _context.Set<Doctor>();
            _cabinets = _context.Set<Cabinet>();
        }

        public override async Task<PaginatedResult<Schedule>> GetAllAsync(
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
                var query = _dbSet.Include(s => s.Doctor).ThenInclude(d => d.Person).Include(s => s.Cabinet).ThenInclude(c => c.Type).AsNoTracking();

                
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

                return new PaginatedResult<Schedule>
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
                _logger.LogWarning("GetAllAsync operation for {Entity} was canceled", typeof(Schedule).Name);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting {Entity} list", typeof(Schedule).Name);
                throw;
            }
        }

        public override async Task<Schedule?> GetByIdAsync(int id, CancellationToken token = default)
        {
            try
            {
                return await _dbSet
                    .Include(s => s.Doctor).ThenInclude(d => d.Person)
                    .Include(s => s.Cabinet).ThenInclude(c => c.Type)
                    .FirstOrDefaultAsync(s => s.Id == id, token);
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning("GetByIdAsync for {Entity} with id {Id} was canceled", typeof(Schedule).Name, id);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving {Entity} with id {Id}", typeof(Schedule).Name, id);
                throw;
            }
        }

        protected override IQueryable<Schedule> ApplySearchFilter(IQueryable<Schedule> query, string searchTerm)
        {
            return query.Where(s =>
                (s.Doctor != null && s.Doctor.Person != null && (
                    s.Doctor.Person.FirstName.Contains(searchTerm) ||
                    s.Doctor.Person.LastName.Contains(searchTerm) ||
                    (s.Doctor.Person.Patronymic != null && s.Doctor.Person.Patronymic.Contains(searchTerm))
                )) ||
                (s.Cabinet != null && (
                    s.Cabinet.Building.ToString().Contains(searchTerm) ||
                    s.Cabinet.Floor.ToString().Contains(searchTerm) ||
                    s.Cabinet.Number.ToString().Contains(searchTerm) ||
                    (s.Cabinet.Type != null && s.Cabinet.Type.Type.Contains(searchTerm))
                ))
            );
        }

        protected override IQueryable<Schedule> ApplySorting(IQueryable<Schedule> query, string sortBy, bool ascending)
        {
            if (sortBy == "Doctor.Person.LastName")
            {
                query = ascending ? query.OrderBy(s => s.Doctor.Person.LastName) 
                    : query.OrderByDescending(s => s.Doctor.Person.LastName);
                return query;
            }
            if (sortBy == "Cabinet.Building")
            {
                query = ascending ? query.OrderBy(s => s.Cabinet.Building) 
                    : query.OrderByDescending(s => s.Cabinet.Building);
                return query;
            }
            if (sortBy == "Cabinet.Floor")
            {
                query = ascending ? query.OrderBy(s => s.Cabinet.Floor) 
                    : query.OrderByDescending(s => s.Cabinet.Floor);
                return query;
            }
            if (sortBy == "Cabinet.Number")
            {
                query = ascending ? query.OrderBy(s => s.Cabinet.Number) 
                    : query.OrderByDescending(s => s.Cabinet.Number);
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

        public async Task<IEnumerable<Cabinet>> GetAllCabinetsAsync(CancellationToken token = default)
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
    }
}

