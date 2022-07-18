using Blockbuster.API.Contexts;
using Blockbuster.API.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Blockbuster.API.Repositories
{
    public abstract class Repository<T> : IRepository<T>
        where T : Entity
    {
        private readonly BlockbusterContext _context;
        protected readonly DbSet<T> DbSet;

        protected Repository(BlockbusterContext context)
        {
            _context = context;
            DbSet = context.Set<T>();
        }

        public ValueTask<EntityEntry<T>> AddAsync(
            T entity,
            CancellationToken cancellationToken) =>
            DbSet.AddAsync(
                entity,
                cancellationToken);

        public void Remove(T entity) =>
            DbSet.Remove(entity);

        public EntityEntry<T> Update(T entity) =>
            DbSet.Update(entity);

        public Task SaveChangesAsync(CancellationToken cancellationToken) =>
            _context.SaveChangesAsync(cancellationToken);

        public Task<int> CountAsync(CancellationToken cancellationToken) =>
            DbSet.CountAsync(cancellationToken);

        public Task<T?> GetByIdAsync(
            long id,
            CancellationToken cancellationToken) =>
            DbSet.SingleOrDefaultAsync(
                p => p.Id == id,
                cancellationToken);

        public Task<T?> GetByIdAsNoTrackingAsync(
            long id,
            CancellationToken cancellationToken) =>
            DbSet.AsNoTracking().SingleOrDefaultAsync(
                p => p.Id == id,
                cancellationToken);
    }
}
