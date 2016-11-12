using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AccountsWebsite.Infrastructure.DataAccess
{
    public class EntityRepository<TEntity> : IEntityRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _context;

        public EntityRepository(DbContext context)
        {
            _context = context;
        }

        public IQueryable<TEntity> Query()
        {
            return _context.Set<TEntity>();
        }

        public async Task<TEntity> FindAsync(int id)
        {
            return await _context.FindAsync<TEntity>(id);
        }

        public async Task<TEntity> GetOneAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().SingleOrDefaultAsync(predicate);
        }

        public async Task<ICollection<TEntity>> GetListAsync(
            Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);

            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);

            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<TEntity> DeleteAsync(int id)
        {
            var entity = await FindAsync(id);

            if (entity != null)
            {
                _context.Remove(entity);

                await _context.SaveChangesAsync();
            }
            else
            {
                entity = null;
            }

            return entity;
        }

        public EntityEntry<TEntity> Add(TEntity entity) => _context.Add(entity);

        public EntityEntry<TEntity> Update(TEntity entity) => _context.Update(entity);

        public EntityEntry<TEntity> Attach(TEntity entity) => _context.Attach(entity);

        public EntityEntry<TEntity> Remove(TEntity entity) => _context.Remove(entity);
    }
}
