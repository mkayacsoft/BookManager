using System.Linq.Expressions;

namespace BookManager.Application.Contracts.Persistence
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> GetAll();
        ValueTask<T?> GetById(int id);
        ValueTask Create(T? entity);
        void Update(T? entity);
        void Delete(T entity);
        Task<IQueryable<T?>> GetWhere(Expression<Func<T?, bool>> predicate);
    }
}
