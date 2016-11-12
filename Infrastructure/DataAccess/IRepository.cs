using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AccountsWebsite.Infrastructure.DataAccess
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> Query();

        Task<TEntity> FindAsync(int id);

        Task<TEntity> GetOneAsync(Expression<Func<TEntity, bool>> predicate);

        Task<ICollection<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> CreateAsync(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity);

        Task<TEntity> DeleteAsync(int id);
    }
}
