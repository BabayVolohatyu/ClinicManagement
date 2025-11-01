using System.Linq.Expressions;

namespace ClinicManagement.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task AddAsync(T entity);
        void Update(T entity);
        void Remove(T entity);
        Task SaveChangesAsync();
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync(int pageNumber = 1, int pageSize = 10);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
    }
}
