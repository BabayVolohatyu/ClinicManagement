using ClinicManagement.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Text;
using System.Reflection;

namespace ClinicManagement.Services
{
    public interface IService<T> where T : class
    {
        Task AddAsync(T entity, CancellationToken token = default);
        Task UpdateAsync(int id, T entity, CancellationToken token = default);
        Task RemoveAsync(int id, CancellationToken token = default);
        Task<T?> GetByIdAsync(int id, CancellationToken token = default);
        public Task<PaginatedResult<T>> GetAllAsync(
         int pageNumber = 1,
         int pageSize = 10,
         string? searchTerm = null,
         string? sortBy = null,
         bool sortAscending = true,
         CancellationToken token = default);
        Task<IEnumerable<T>> FindAsync(
            Expression<Func<T, bool>> predicate,
            CancellationToken token = default);
        Task<byte[]> ExportToCsvAsync(CancellationToken token = default);
    }
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
         string? searchTerm = null,
         string? sortBy = null,
         bool sortAscending = true,
         CancellationToken token = default)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1 || pageSize > 100) pageSize = 10;

            try
            {
                var query = _dbSet.AsNoTracking();

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

                return new PaginatedResult<T>
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
                _logger.LogWarning("GetAllAsync operation for {Entity} was canceled", typeof(T).Name);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting {Entity} list", typeof(T).Name);
                throw;
            }
        }

        // Virtual method for search filtration - can be overridden in child services
        protected virtual IQueryable<T> ApplySearchFilter(IQueryable<T> query, string searchTerm)
        {
            // Default implementation searches in all string properties
            var stringProperties = typeof(T).GetProperties()
                .Where(p => p.PropertyType == typeof(string) && p.CanRead);

            if (!stringProperties.Any())
                return query;

            // Build OR expression for all string properties
            var parameter = Expression.Parameter(typeof(T), "x");
            Expression? orExpression = null;

            foreach (var property in stringProperties)
            {
                var propertyAccess = Expression.Property(parameter, property);
                var constant = Expression.Constant(searchTerm.ToLower());
                var toLowerMethod = typeof(string).GetMethod("ToLower", Type.EmptyTypes);
                var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });

                if (toLowerMethod != null && containsMethod != null)
                {
                    var toLowerCall = Expression.Call(propertyAccess, toLowerMethod);
                    var containsCall = Expression.Call(toLowerCall, containsMethod, constant);

                    orExpression = orExpression == null
                        ? containsCall
                        : Expression.OrElse(orExpression, containsCall);
                }
            }

            if (orExpression != null)
            {
                var lambda = Expression.Lambda<Func<T, bool>>(orExpression, parameter);
                query = query.Where(lambda);
            }

            return query;
        }

        // Virtual method for sorting - can be overridden in child services
        protected virtual IQueryable<T> ApplySorting(IQueryable<T> query, string sortBy, bool ascending)
        {
            var property = typeof(T).GetProperty(sortBy,
                System.Reflection.BindingFlags.IgnoreCase |
                System.Reflection.BindingFlags.Public |
                System.Reflection.BindingFlags.Instance);

            if (property == null)
            {
                // Fallback to ID sorting if specified property doesn't exist
                property = typeof(T).GetProperty("Id");
                if (property == null) return query;
            }

            var parameter = Expression.Parameter(typeof(T), "x");
            var propertyAccess = Expression.Property(parameter, property);
            var orderByExpression = Expression.Lambda(propertyAccess, parameter);

            string methodName = ascending ? "OrderBy" : "OrderByDescending";
            var resultExpression = Expression.Call(
                typeof(Queryable),
                methodName,
                new[] { typeof(T), property.PropertyType },
                query.Expression,
                Expression.Quote(orderByExpression));

            return query.Provider.CreateQuery<T>(resultExpression);
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
                var entity = await GetByIdAsync(id, token);
                if (entity == null)
                    throw new KeyNotFoundException($"{typeof(T).Name} with id {id} not found");

                _dbSet.Remove(entity);
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

        public virtual async Task<byte[]> ExportToCsvAsync(CancellationToken token = default)
        {
            try
            {
                // Get all entities using pagination
                var allEntities = new List<T>();
                int pageNumber = 1;
                const int pageSize = 100; // Max allowed page size
                PaginatedResult<T> result;

                do
                {
                    result = await GetAllAsync(pageNumber, pageSize, null, "Id", true, token);
                    allEntities.AddRange(result.Items);
                    pageNumber++;
                } while (result.Items.Any() && allEntities.Count < result.TotalCount);

                // Get all simple properties (primitives, strings, dates, numbers)
                // Exclude navigation properties and collections - just export raw data
                var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(p => p.CanRead)
                    .Where(p => IsSimpleProperty(p.PropertyType))
                    .OrderBy(p => p.Name == "Id" ? 0 : 1) // Put Id first
                    .ThenBy(p => p.Name)
                    .ToList();

                // Build CSV content
                var csv = new StringBuilder();

                // Add header row
                var headers = properties.Select(p => p.Name);
                csv.AppendLine(string.Join(",", headers));

                // Add data rows
                foreach (var entity in allEntities)
                {
                    var values = properties.Select(prop =>
                    {
                        var value = prop.GetValue(entity);
                        if (value == null) return "";
                        
                        // Format dates consistently
                        if (value is DateTime dt) return dt.ToString("yyyy-MM-dd HH:mm:ss");
                        if (value is DateTimeOffset dto) return dto.ToString("yyyy-MM-dd HH:mm:ss");
                        
                        return EscapeCsvField(value.ToString() ?? "");
                    });
                    csv.AppendLine(string.Join(",", values));
                }

                // Convert to bytes
                return Encoding.UTF8.GetBytes(csv.ToString());
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning("ExportToCsvAsync operation for {Entity} was canceled", typeof(T).Name);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while exporting {Entity} to CSV", typeof(T).Name);
                throw;
            }
        }

        private bool IsSimpleProperty(Type type)
        {
            // Handle nullable types
            var underlyingType = Nullable.GetUnderlyingType(type);
            if (underlyingType != null)
                type = underlyingType;

            // Only include: primitives, string, DateTime, DateTimeOffset, decimal, Guid
            return type.IsPrimitive || 
                   type == typeof(string) || 
                   type == typeof(DateTime) || 
                   type == typeof(DateTimeOffset) || 
                   type == typeof(decimal) || 
                   type == typeof(Guid);
        }

        private string EscapeCsvField(string? field)
        {
            if (string.IsNullOrEmpty(field))
                return "";

            // If field contains comma, quote, or newline, wrap in quotes and escape quotes
            if (field.Contains(',') || field.Contains('"') || field.Contains('\n') || field.Contains('\r'))
            {
                return "\"" + field.Replace("\"", "\"\"") + "\"";
            }

            return field;
        }
    }
}
