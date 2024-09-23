using LoremIpsumLogistica.Server.Infraestructure.Entities;
using LoremIpsumLogistica.Server.Infraestructure.Repositories;

namespace LoremIpsumLogistica.Server.Infraestructure.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<T> Repository<T>() where T : BaseEntity;
        
        Task CommitAsync();
        
        Task BeginTransactionAsync();
        
        Task CommitTransactionAsync();
        
        Task RollbackTransactionAsync();
    }
}