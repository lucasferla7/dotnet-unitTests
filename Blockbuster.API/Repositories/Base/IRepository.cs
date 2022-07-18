using Blockbuster.API.Domain;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Blockbuster.API.Repositories
{
    public interface IRepository<T>
        where T : Entity
    {
        ValueTask<EntityEntry<T>> AddAsync(
            T entity,
            CancellationToken cancellationToken);

        EntityEntry<T> Update(T entity);

        void Remove(T entity);

        Task<T?> GetByIdAsync(
            long id,
            CancellationToken cancellationToken);

        Task<T?> GetByIdAsNoTrackingAsync(
            long id,
            CancellationToken cancellationToken);

        Task SaveChangesAsync(CancellationToken cancellationToken);

        Task<int> CountAsync(CancellationToken cancellationToken);
    }
}
