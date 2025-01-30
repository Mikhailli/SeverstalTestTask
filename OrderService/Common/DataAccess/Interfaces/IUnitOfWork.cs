using Common.DataAccess.Implementations;

namespace Common.DataAccess.Interfaces;

internal interface IUnitOfWork : IDisposable
{
    int Commit();

    void BulkInsert<TEntity>(IList<TEntity> entities) where TEntity : Entity;
}
