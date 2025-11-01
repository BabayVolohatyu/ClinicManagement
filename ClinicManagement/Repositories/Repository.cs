using ClinicManagement.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ClinicManagement.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ClinicDbContext _context;
        protected readonly DbSet<T> _dbSet; 

        public Repository(ClinicDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
           return await _dbSet
                .Where(predicate)
                .ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(int pageNumber = 1, int pageSize = 10)
        {
            if(pageNumber < 1) pageNumber = 1;
            if(pageSize < 1) pageSize = 1;
            return await _dbSet
                .Skip((pageNumber-1)*pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id) =>
            await _dbSet.FindAsync(id);

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task SaveChangesAsync() =>
            await _context.SaveChangesAsync();
        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }
    }
}
