using System.Data;

namespace MyRoad.Domain.Identity.Interfaces;

public interface IUnitOfWork
{
    Task BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}