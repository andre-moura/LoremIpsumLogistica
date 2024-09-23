using LoremIpsumLogistica.Server.Infraestructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LoremIpsumLogistica.Server.Infraestructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly DbContext _context;
        protected readonly DbSet<T> _entities;

        public BaseRepository(DbContext context)
        {
            _context = context;
            _entities = _context.Set<T>();
        }

        public async Task<T?> GetByIdAsync(int id, Func<IQueryable<T>, IQueryable<T>>? include = null)
        {
            IQueryable<T> query = _entities;

            if (include != null)
            {
                query = include(query);
            }

            return await query.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<T>> GetAllAsync(Func<IQueryable<T>, IQueryable<T>>? include = null)
        {
            IQueryable<T> query = _entities;

            if (include != null)
            {
                query = include(query);
            }

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate) =>
            await _entities.Where(predicate).ToListAsync();

        public async Task AddAsync(T entity) => await _entities.AddAsync(entity);

        public async Task UpdateAsync(T entity) => _entities.Update(entity);

        public async Task DeleteAsync(T entity) => _entities.Remove(entity);
    }
}