using ClinicManagement.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ClinicManagement.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ClinicDbContext _context;
        protected readonly DbSet<T> _dbSet; 

        public Repository(ClinicDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public virtual async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
           return await _dbSet
                .Where(predicate)
                .ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(int pageNumber = 1, int pageSize = 10)
        {
            return await _dbSet
                .Skip((pageNumber-1)*pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public virtual async Task<T?> GetByIdAsync(int id) =>
            await _dbSet.FindAsync(id);

        public virtual void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public virtual async Task SaveChangesAsync() =>
            await _context.SaveChangesAsync();
        public virtual void Update(T entity)
        {
            _dbSet.Update(entity);
        }
    }
}
