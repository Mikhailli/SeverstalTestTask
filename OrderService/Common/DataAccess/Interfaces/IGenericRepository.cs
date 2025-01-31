using System.Linq.Expressions;
using Common.DataAccess.Implementations;
using Microsoft.EntityFrameworkCore.Query;

namespace Common.DataAccess.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : Entity
    {
        public TEntity? GetById(object? id);

        public Task<TEntity?> GetByIdAsync(object? id);

        public IEnumerable<TEntity> GetAll();

        internal IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>>? filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>[]? includes = null, int? skip = null, int? take = null);

        internal int GetCount(Expression<Func<TEntity, bool>>? predicate = null);

        internal TEntity Add(TEntity entity);

        internal void Update(TEntity entity);

        internal void Delete(TEntity entity);
    }
}
