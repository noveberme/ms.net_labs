using System.Linq.Expressions;
using TicketsFilm.DataAccess.Entities;

namespace TicketsFilm.DataAccess.Repository;

public class IRepository<T> where T : BaseEntity
{
    IQueryable<T> GetAll();
    IQueryable<T> GetAll(Expression<Func<T, bool>> predicate);
    T? GetById(int id);
    T? GetById(Guid id);
    T Save(T entity);
    void Delete(T entity);
}