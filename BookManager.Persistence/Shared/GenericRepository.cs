using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using BookManager.Application.Contracts.Persistence;

namespace BookManager.Persistence.Shared
{
    public class GenericRepository<T>(ApplicationDbContext context):IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context = context;
        private readonly DbSet<T?> _dbSet= context.Set<T>();

        public async Task<List<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async ValueTask<T?> GetById(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async ValueTask Create(T? entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Update(T? entity)
        {
            _dbSet.Update(entity);
        }

        public void Delete(T? entity)
        {
            _dbSet.Remove(entity);
        }

        public Task<IQueryable<T?>> GetWhere(Expression<Func<T?, bool>> predicate)
        {
            return  Task.FromResult(_dbSet.Where(predicate).AsNoTracking());
        }






    }

}
