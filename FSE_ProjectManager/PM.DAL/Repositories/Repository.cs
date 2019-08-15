using PM.DAL.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace PM.DAL.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity>
       where TEntity : class
    {
        protected readonly PMDbContext Context = PMDbContext.Create();

        public virtual TEntity Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);

            return entity;
        }

        public virtual IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().AddRange((IEnumerable<TEntity>)entities);

            return entities;
        }

        public virtual IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate,
            int pageIndex = -1, int pageSize = 0)
        {
            var query = Context.Set<TEntity>().Where(predicate);

            if (pageIndex > -1)
            {
                return query
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize);
            }

            return query;
        }

        public virtual TEntity Get(int id)
        {
            return Context.Set<TEntity>().Find(id);
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return Context.Set<TEntity>();
        }

        public void Remove(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().RemoveRange(entities);
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }
    }
}
