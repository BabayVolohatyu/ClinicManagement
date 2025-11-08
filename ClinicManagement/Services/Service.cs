using ClinicManagement.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ClinicManagement.Services
{
    public abstract class Service<T> : IService<T> where T : class
    {
        protected readonly ClinicDbContext _context;
        protected readonly DbSet<T> _dbSet;
        protected readonly ILogger<Service<T>> _logger;

        protected Service(ClinicDbContext context, ILogger<Service<T>> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<T>();
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public virtual async Task<PaginatedResult<T>> GetAllAsync(
            int pageNumber = 1,
            int pageSize = 10,
            CancellationToken token = default)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1 || pageSize > 100) pageSize = 10;

            try
            {
                var items = await _dbSet
                    .AsNoTracking()
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync(token);

                var totalCount = await _dbSet.CountAsync(token);

                return new PaginatedResult<T>
                {
                    Items = items,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalCount = totalCount
                };
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning("GetAllAsync operation for {Entity} was canceled", typeof(T).Name);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting {Entity} list", typeof(T).Name);
                throw;
            }
        }

        public virtual async Task<T?> GetByIdAsync(int id, CancellationToken token = default)
        {
            try
            {
                return await _dbSet.FindAsync([id], token);
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning("GetByIdAsync for {Entity} with id {Id} was canceled", typeof(T).Name, id);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving {Entity} with id {Id}", typeof(T).Name, id);
                throw;
            }
        }

        public virtual async Task<IEnumerable<T>> FindAsync(
            Expression<Func<T, bool>> predicate,
            CancellationToken token = default)
        {
            try
            {
                return await _dbSet
                    .AsNoTracking()
                    .Where(predicate)
                    .ToListAsync(token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while filtering {Entity}", typeof(T).Name);
                throw;
            }
        }

        public virtual async Task AddAsync(T entity, CancellationToken token = default)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                await _dbSet.AddAsync(entity, token);
                await _context.SaveChangesAsync(token);

                _logger.LogInformation("Added new {Entity}", typeof(T).Name);
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning("AddAsync for {Entity} was canceled", typeof(T).Name);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding {Entity}", typeof(T).Name);
                throw;
            }
        }

        public virtual async Task UpdateAsync(int id, T entity, CancellationToken token = default)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                var existingEntity = await GetByIdAsync(id, token);
                if (existingEntity == null)
                    throw new KeyNotFoundException($"{typeof(T).Name} with id {id} not found");

                _context.Entry(existingEntity).CurrentValues.SetValues(entity);
                _context.Entry(existingEntity).State = EntityState.Modified;

                await _context.SaveChangesAsync(token);

                _logger.LogInformation("Updated {Entity} with id {Id}", typeof(T).Name, id);
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning("UpdateAsync for {Entity} with id {Id} was canceled", typeof(T).Name, id);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating {Entity} with id {Id}", typeof(T).Name, id);
                throw;
            }
        }

        public virtual async Task RemoveAsync(int id, CancellationToken token = default)
        {
            try
            {
                var entity = Activator.CreateInstance<T>();
                var keyProperty = _context.Model.FindEntityType(typeof(T))?.FindPrimaryKey()?.Properties.FirstOrDefault();
                if (keyProperty == null)
                    throw new InvalidOperationException($"No key defined for entity {typeof(T).Name}");

                _context.Entry(entity!).Property(keyProperty.Name).CurrentValue = id;
                _context.Entry(entity!).State = EntityState.Deleted;

                await _context.SaveChangesAsync(token);

                _logger.LogInformation("Removed {Entity} with id {Id}", typeof(T).Name, id);
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning("RemoveAsync for {Entity} with id {Id} was canceled", typeof(T).Name, id);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while removing {Entity} with id {Id}", typeof(T).Name, id);
                throw;
            }
        }
    }
}
