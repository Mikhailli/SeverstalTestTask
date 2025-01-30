using System.Linq.Expressions;
using Common.DataAccess.Implementations;
using Microsoft.EntityFrameworkCore.Query;

namespace Common.DataAccess.Interfaces
{
    internal interface IGenericRepository<TEntity> where TEntity : Entity
    {
        TEntity? GetById(object? id);

        Task<TEntity?> GetByIdAsync(object? id);

        IEnumerable<TEntity> GetAll();

        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>>? filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>[]? includes = null, int? skip = null, int? take = null);

        int GetCount(Expression<Func<TEntity, bool>>? predicate = null);

        TEntity Add(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);
    }
}
