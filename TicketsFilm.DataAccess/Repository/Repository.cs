using System.Linq.Expressions;
using TicketsFilm.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace TicketsFilm.DataAccess.Repository;

public class Repository <T> : IRepository<T> where T :  class, IBaseEntity
{
    private readonly IDbContextFactory<DbContext> _contextFactory;

    public Repository(IDbContextFactory<DbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public IQueryable<T> GetAll()
    {
        using var context = _contextFactory.CreateDbContext();
        return context.Set<T>();
    }

    public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate)
    {
        using var context = _contextFactory.CreateDbContext();
        return context.Set<T>().Where(predicate);
    }

    public T? GetById(int id)
    {
        using var context = _contextFactory.CreateDbContext();
        return context.Set<T>().FirstOrDefault(x => x.Id == id);
    }

    public T? GetById(Guid id)
    {
        using var context = _contextFactory.CreateDbContext();
        return context.Set<T>().FirstOrDefault(x => x.ExternalId == id);
    }

    public T Save(T entity)
    {
        using var context = _contextFactory.CreateDbContext();
        if (context.Set<T>().Any(x => x.Id == entity.Id))
        {
            entity.ModificationTime = DateTime.UtcNow;
            var result = context.Set<T>().Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
            return result.Entity;
        }
        else
        {
            entity.ExternalId = Guid.NewGuid();
            entity.CreationTime = DateTime.UtcNow;
            entity.ModificationTime = entity.CreationTime;
            var result = context.Set<T>().Add(entity);
            context.SaveChanges();
            return result.Entity;
        }
    }

    public void Delete(T entity)
    {
        using var context = _contextFactory.CreateDbContext();
        context.Set<T>().Attach(entity);
        context.Entry(entity).State = EntityState.Deleted;
        context.SaveChanges();
    }
}