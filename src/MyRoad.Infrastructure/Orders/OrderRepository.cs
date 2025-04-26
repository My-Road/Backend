using Microsoft.EntityFrameworkCore;
using MyRoad.Domain.Common.Entities;
using MyRoad.Domain.Orders;
using MyRoad.Infrastructure.Persistence;
using Sieve.Models;
using Sieve.Services;

namespace MyRoad.Infrastructure.Orders;

public class OrderRepository(
    AppDbContext dbContext,
    ISieveProcessor sieveProcessor
) : IOrderRepository
{
    public async Task<bool> CreateAsync(Order order)
    {
        await dbContext.Orders.AddAsync(
            new Order
            {
                OrderDate = order.OrderDate,
                Notes = order.Notes,
                Price = order.Price,
                Quantity = order.Quantity,
                RecipientName = order.RecipientName,
                RecipientPhoneNumber = order.RecipientPhoneNumber,
                CustomerId = order.CustomerId,
                CreatedByUserId = order.CreatedByUserId,
                IsCompleted = order.IsCompleted
            }
        );
        return await dbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateAsync(Order order)
    {
        dbContext.Orders.Update(order);
        return await dbContext.SaveChangesAsync() > 0;
    }

    public async Task<Order?> GetByIdAsync(long orderId)
    {
        var order = await dbContext.Orders.FindAsync(orderId);
        return order;
    }

    public async Task<PaginatedResponse<Order>> GetAsync(SieveModel sieveModel)
    {
        var query = dbContext.Orders.AsQueryable();

        var totalItems = await sieveProcessor
            .Apply(sieveModel, query, applyPagination: false)
            .CountAsync();

        query = sieveProcessor.Apply(sieveModel, query);

        var result = await query.AsNoTracking().ToListAsync();

        return new PaginatedResponse<Order>
        {
            Items = result,
            TotalCount = totalItems,
            Page = sieveModel.Page ?? 1,
            PageSize = sieveModel.PageSize ?? 10,
        };
    }

    public async Task<PaginatedResponse<Order>> GetByCustomerAsync(long customerId, SieveModel sieveModel)
    {
        var query = dbContext.Orders
            .Where(p => p.CustomerId == customerId && !p.IsDeleted)
            .AsQueryable();

        var totalItems = await sieveProcessor
            .Apply(sieveModel, query, applyPagination: false)
            .CountAsync();

        query = sieveProcessor.Apply(sieveModel, query);

        var result = await query.AsNoTracking().ToListAsync();

        return new PaginatedResponse<Order>
        {
            Items = result,
            TotalCount = totalItems,
            Page = sieveModel.Page ?? 1,
            PageSize = sieveModel.PageSize ?? 10,
        };
    }
}