using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess.EntityRepository
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext, new()
    {
        public void Add(TEntity TEntity)
        {
            using (var Context=new TContext())
            {
              var addedEntity=  Context.Entry(TEntity);
                addedEntity.State=EntityState.Added;
                Context.SaveChanges();
            }
        }

        public void Delete(TEntity TEntity)
        {
            using (var Context = new TContext())
            {
                var deletedEntity = Context.Entry(TEntity);
                deletedEntity.State = EntityState.Deleted;
                Context.SaveChanges();
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (var Context = new TContext())
            {
                return Context.Set<TEntity>().SingleOrDefault(filter);
            }
        }

        public IList<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null)
        {
            using (var Context = new TContext())
            {
                return filter == null ? Context.Set<TEntity>().ToList() : Context.Set<TEntity>().Where(filter).ToList();            
            }
        }

        public void Update(TEntity TEntity)
        {
            using (var Context = new TContext())
            {
                var updatedEntity = Context.Entry(TEntity);
                updatedEntity.State = EntityState.Modified;
                Context.SaveChanges();
            }
        }
    }
}
