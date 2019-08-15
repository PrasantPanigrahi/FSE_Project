using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PM.DAL.Repositories.Interface
{
    public interface IRepository<TEntity>
      where TEntity : class
    {
        /// <summary>
        /// Get single entity
        /// </summary>
        TEntity Get(int id);

        IQueryable<TEntity> GetAll();

        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate,
            int pageIndex = -1, int pageSize = 0);

        TEntity Add(TEntity entity);

        IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);

        void RemoveRange(IEnumerable<TEntity> entities);

        void SaveChanges();
    }
}