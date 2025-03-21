using MyRoad.Domain.Identity.Interfaces;
using MyRoad.Infrastructure.Persistence;

namespace MyRoad.Infrastructure.Identity;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    public async Task BeginTransactionAsync()
    {
        await context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        await context.Database.CommitTransactionAsync();
    }

    public async Task RollbackTransactionAsync()
    {
        await context.Database.RollbackTransactionAsync();
    }
}