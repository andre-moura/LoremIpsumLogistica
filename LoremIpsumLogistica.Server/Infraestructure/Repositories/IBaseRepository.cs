using LoremIpsumLogistica.Server.Infraestructure.Entities;
using System.Linq.Expressions;

namespace LoremIpsumLogistica.Server.Infraestructure.Repositories
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<T?> GetByIdAsync(int id, Func<IQueryable<T>, IQueryable<T>> include = null);

        Task<IEnumerable<T>> GetAllAsync(Func<IQueryable<T>, IQueryable<T>>? include = null);
        
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        
        Task AddAsync(T entity);
        
        Task UpdateAsync(T entity);
        
        Task DeleteAsync(T entity);
    }
}