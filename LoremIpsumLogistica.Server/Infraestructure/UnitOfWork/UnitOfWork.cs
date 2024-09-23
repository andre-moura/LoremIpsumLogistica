using LoremIpsumLogistica.Server.Infraestructure.Entities;
using LoremIpsumLogistica.Server.Infraestructure.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace LoremIpsumLogistica.Server.Infraestructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly Dictionary<Type, object> _repositories;
        private IDbContextTransaction _transaction;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            _repositories = new Dictionary<Type, object>();
        }

        public IBaseRepository<T> Repository<T>() where T : BaseEntity
        {
            if (!_repositories.ContainsKey(typeof(T)))
            {
                var repositoryInstance = new BaseRepository<T>(_context);
                _repositories[typeof(T)] = repositoryInstance;
            }

            return (IBaseRepository<T>)_repositories[typeof(T)];
        }

        public Task CommitAsync() => _context.SaveChangesAsync();

        public async Task BeginTransactionAsync() =>
            _transaction = await _context.Database.BeginTransactionAsync();

        public async Task CommitTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
                _transaction = null;
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                _transaction = null;
            }
        }

        public void Dispose() => _context.Dispose();
    }
}