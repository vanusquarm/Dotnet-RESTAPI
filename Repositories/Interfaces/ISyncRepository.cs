using System;
using System.Linq;
using System.Linq.Expressions;

namespace WebApplication.API.Repositories.Interfaces;
public interface ISyncRepository<TEntity> where TEntity: class
{
    public TEntity GetById(int id);
    public IQueryable<TEntity> GetAll();
    public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter);

    public void Add(TEntity entity);

    public void Update(TEntity entity);

    public void Delete(int id);
    public void SaveChanges();
    // other repository methods
}