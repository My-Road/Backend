using Microsoft.EntityFrameworkCore;
using MyRoad.Domain.Common.Entities;
using MyRoad.Domain.Orders;
using MyRoad.Domain.Payments.CustomerPayments;
using MyRoad.Infrastructure.Persistence;
using Sieve.Models;
using Sieve.Services;

namespace MyRoad.Infrastructure.Payments.CustomerPayments;

public class CustomerPaymentRepository(
    AppDbContext dbContext,
    ISieveProcessor sieveProcessor
) : ICustomerPaymentRepository
{
    public async Task<bool> CreateAsync(CustomerPayment customerPayment)
    {
        await dbContext.CustomerPayments.AddAsync(
            customerPayment
        );
        return await dbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateAsync(CustomerPayment customerPayment)
    {
        dbContext.CustomerPayments.Update(customerPayment);
        return await dbContext.SaveChangesAsync() > 0;
    }

    public async Task<CustomerPayment?> GetByIdAsync(long id)
    {
        var payment = await dbContext.CustomerPayments.FindAsync(id);
        return payment;
    }

    public async Task<PaginatedResponse<CustomerPayment>> GetAsync(SieveModel sieveModel)
    {
        var query = dbContext.CustomerPayments
            .Include(o => o.Customer)
            .AsQueryable();

        var totalItems = await sieveProcessor
            .Apply(sieveModel, query, applyPagination: false)
            .CountAsync();

        query = sieveProcessor.Apply(sieveModel, query);

        var result = await query.AsNoTracking().ToListAsync();

        return new PaginatedResponse<CustomerPayment>
        {
            Items = result,
            TotalCount = totalItems,
            Page = sieveModel.Page ?? 1,
            PageSize = sieveModel.PageSize ?? 10,
        };
    }

    public async Task<PaginatedResponse<CustomerPayment>> GetByCustomerAsync(long id, SieveModel sieveModel)
    {
        var query = dbContext.CustomerPayments
            .Where(p => p.CustomerId == id && !p.IsDeleted)
            .AsQueryable();

        var totalItems = await sieveProcessor
            .Apply(sieveModel, query, applyPagination: false)
            .CountAsync();

        query = sieveProcessor.Apply(sieveModel, query);

        var result = await query.AsNoTracking().ToListAsync();

        return new PaginatedResponse<CustomerPayment>
        {
            Items = result,
            TotalCount = totalItems,
            Page = sieveModel.Page ?? 1,
            PageSize = sieveModel.PageSize ?? 10,
        };
    }
}