using System.Linq.Expressions;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.DataAccess.EntityFramework;

public class EfEntityRepositoryBase<TEntity, TContext> :IEntityRepository<TEntity>
    where TEntity : class, IEntity, new()
    where TContext : DbContext, new()
{
    public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
    {
        using (TContext context = new TContext())
        {
            return filter == null 
                ? context.Set<TEntity>().ToList() 
                : context.Set<TEntity>().Where(filter).ToList();     
        }
    }
    
    public List<TEntity> GetProductsById(Expression<Func<TEntity, bool>> filter = null)
    {
        using (TContext context = new TContext())
        {
            return filter == null 
                ? context.Set<TEntity>().ToList() 
                : context.Set<TEntity>().Where(filter).ToList();     
        }
    }

    public TEntity Get(Expression<Func<TEntity, bool>> filter)
    {
        using (TContext context = new TContext())
        {
            return context.Set<TEntity>().SingleOrDefault(filter);
        }
    }

    public void Add(TEntity entity)
    {
        //IDisposable pattern implementation of c#
        using (TContext context = new TContext())
        {
            //to catch the reference
            var addedEntity = context.Entry(entity);
            addedEntity.State = EntityState.Added;
            context.SaveChanges();
        }
    }

    public void Delete(TEntity entity)
    {
        using (TContext context = new TContext())
        {
            //to catch the reference
            var deletedEntity = context.Entry(entity);
            deletedEntity.State = EntityState.Deleted;
            context.SaveChanges();
        }
    }

    public void Update(TEntity entity)
    {
        using (TContext context = new TContext())
        {
            //to catch the reference
            var modifiedEntity = context.Entry(entity);
            modifiedEntity.State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}