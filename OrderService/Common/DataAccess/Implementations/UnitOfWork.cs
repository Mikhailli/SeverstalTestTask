using Common.DataAccess.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Storage;
using Tanneryd.BulkOperations.EFCore;

namespace Common.DataAccess.Implementations;

public class UnitOfWork(OrderDbContext dbContext) : IUnitOfWork
{
    private OrderDbContext? _dbContext = dbContext;

    public int Commit()
    {
        return _dbContext!.SaveChanges();
    }

    public void BulkInsert<TEntity>(IList<TEntity> entities) where TEntity : Entity
    {
        using var transaction = _dbContext!.Database.BeginTransaction();
        try
        {
            var sqlTransaction = (SqlTransaction)transaction.GetDbTransaction();
            _dbContext.BulkInsertAll(entities, sqlTransaction, true);
            transaction.Commit();
        }
        catch (Exception)
        {
            transaction.Rollback();
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (_dbContext is not null)
            {
                _dbContext.Dispose();
                _dbContext = null;
            }
        }
    }
}
