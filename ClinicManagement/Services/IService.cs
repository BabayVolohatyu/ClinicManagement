using System.Linq.Expressions;

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
            CancellationToken token = default);
        Task<IEnumerable<T>> FindAsync(
            Expression<Func<T, bool>> predicate,
            CancellationToken token = default);
    }
}
