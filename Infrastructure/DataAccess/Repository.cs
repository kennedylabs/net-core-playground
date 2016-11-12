using AccountsWebsite.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AccountsWebsite.Infrastructure.DataAccess
{
    public static class Repository
    {
        public static async Task<TEntity> EnsureFindAsync<TEntity>(
            this IRepository<TEntity> repo, int id) where TEntity : class
        {
            var entity = await repo.FindAsync(id);

            if (entity == null) throw new NotFoundException();

            return entity;
        }

        public static async Task<TEntity> EnsureGetOneAsync<TEntity>(
            this IRepository<TEntity> repo, Expression<Func<TEntity, bool>> predicate)
            where TEntity : class
        {
            var entity = await repo.GetOneAsync(predicate);

            if (entity == null) throw new NotFoundException();

            return entity;
        }

        public static async Task<ICollection<TEntity>> EnsureGetListAsync<TEntity>(
            this IRepository<TEntity> repo, Expression<Func<TEntity, bool>> predicate)
            where TEntity : class
        {
            var entities = await repo.GetListAsync(predicate);

            if (entities == null) throw new NotFoundException();

            return entities;
        }

        public static async Task<T> SelectPropertyAsync<TEntity, T>(
            this IRepository<TEntity> repo, Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, T>> selector)
            where TEntity : class
        {
            return await repo.Query()
                .Where(predicate)
                .Select(selector)
                .FirstOrDefaultAsync();
        }

        public static async Task<Tuple<T1, T2>> SelectPropertyAsync<TEntity, T1, T2>(
            this IRepository<TEntity> repo, Expression<Func<TEntity, bool>> predicate,
            Func<TEntity, T1> selector1, Func<TEntity, T2> selector2)
            where TEntity : class
        {
            var result = await repo.Query()
                .Where(predicate)
                .Select(e => new { Property1 = selector1(e), Property2 = selector2(e) })
                .FirstOrDefaultAsync();

            return result == null ? null : Tuple.Create(result.Property1, result.Property2);
        }

        public static EntityEntry<TEntity> UpdateProperty<TEntity, T>(
            this EntityEntry<TEntity> entry, Expression<Func<TEntity, T>> propertySelector,
            T value) where TEntity : class
        {
            var propertyName = (propertySelector.Body as MemberExpression)?.Member?.Name;
            var property = entry.Property<T>(propertyName);

            if (property != null && !(property.CurrentValue == null && value == null) &&
                !(property.CurrentValue != null && property.CurrentValue.Equals(value)))
            {
                property.CurrentValue = value;
                property.IsModified = true;
            }

            return entry;
        }

        public static EntityEntry<TEntity> UpdateProperties<TEntity, T1>(
            this EntityEntry<TEntity> entry, TEntity newEntity,
            Expression<Func<TEntity, T1>> propertySelector1)
            where TEntity : class
        {
            return entry.UpdateProperty(propertySelector1, propertySelector1.Compile()(newEntity));
        }

        public static EntityEntry<TEntity> UpdateProperties<TEntity, T1, T2>(
            this EntityEntry<TEntity> entry, TEntity newEntity,
            Expression<Func<TEntity, T1>> propertySelector1,
            Expression<Func<TEntity, T2>> propertySelector2)
            where TEntity : class
        {
            return entry.UpdateProperties(newEntity, propertySelector1)
                .UpdateProperties(newEntity, propertySelector2);
        }

        public static EntityEntry<TEntity> UpdateProperties<TEntity, T1, T2, T3>(
            this EntityEntry<TEntity> entry, TEntity newEntity,
            Expression<Func<TEntity, T1>> propertySelector1,
            Expression<Func<TEntity, T2>> propertySelector2,
            Expression<Func<TEntity, T3>> propertySelector3)
            where TEntity : class
        {
            return entry.UpdateProperties(newEntity, propertySelector1, propertySelector2)
                .UpdateProperties(newEntity, propertySelector3);
        }

        public static EntityEntry<TEntity> UpdateProperties<TEntity, T1, T2, T3, T4>(
            this EntityEntry<TEntity> entry, TEntity newEntity,
            Expression<Func<TEntity, T1>> propertySelector1,
            Expression<Func<TEntity, T2>> propertySelector2,
            Expression<Func<TEntity, T3>> propertySelector3,
            Expression<Func<TEntity, T4>> propertySelector4)
            where TEntity : class
        {
            return entry.UpdateProperties(newEntity, propertySelector1, propertySelector2,
                propertySelector3).UpdateProperties(newEntity, propertySelector4);
        }
    }
}
