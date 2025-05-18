using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MyRoad.Domain.Identity.Interfaces;
using MyRoad.Infrastructure.Persistence;

namespace MyRoad.Infrastructure.Identity;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    private IDbContextTransaction? _transaction;

    public async Task BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
    {
        _transaction = await context.Database.BeginTransactionAsync(isolationLevel);

        await Task.CompletedTask;
    }

    public async Task CommitTransactionAsync()
    {
        if (_transaction is null)
            throw new InvalidOperationException("Transaction has not been started.");

        await _transaction.CommitAsync();
        await _transaction.DisposeAsync();
    }

    public async Task RollbackTransactionAsync()
    {
        if (_transaction is null)
            throw new InvalidOperationException("Transaction has not been started.");
        
        await _transaction.RollbackAsync();
        await _transaction.DisposeAsync();
    }
}