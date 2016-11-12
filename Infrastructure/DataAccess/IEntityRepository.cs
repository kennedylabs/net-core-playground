using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AccountsWebsite.Infrastructure.DataAccess
{
    public interface IEntityRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        EntityEntry<TEntity> Add(TEntity entity);

        EntityEntry<TEntity> Update(TEntity entity);

        EntityEntry<TEntity> Attach(TEntity entity);

        EntityEntry<TEntity> Remove(TEntity entity);
    }
}
