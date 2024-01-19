using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace WebApplication.API.Repositories {

    public class BaseRepositorySync<TEntity> where TEntity : class
    {
        protected readonly DbContext _dbContext;

        public BaseRepositorySync(DbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public TEntity GetById(int id)
        {
            return _dbContext.Set<TEntity>().Find(id);
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>();
        }

        public (IQueryable<TEntity> Items, int TotalCount) GetAll(int pageNumber, int pageSize)
        {
            var query = _dbContext.Set<TEntity>().AsQueryable();
            var total = query.Count();

            var items = query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return (items.AsQueryable(), total);
        }

        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter)
        {
            return _dbContext.Set<TEntity>().Where(filter);
        }

        public void Add(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
        }

        public void Update(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var entity = _dbContext.Set<TEntity>().Find(id);
            if (entity != null)
                _dbContext.Set<TEntity>().Remove(entity);
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }

}
