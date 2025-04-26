using ErrorOr;
using Microsoft.EntityFrameworkCore;
using MyRoad.Domain.Common.Entities;
using MyRoad.Domain.Customers;
using MyRoad.Infrastructure.Persistence;
using Sieve.Models;
using Sieve.Services;

namespace MyRoad.Infrastructure.Customers;

public class CustomerRepository(
    AppDbContext dbContext,
    ISieveProcessor sieveProcessor) : ICustomerRepository
{
    public async Task<bool> CreateAsync(Customer customer)
    {
        await dbContext.Customers.AddAsync(
            new Customer
            {
                FullName = customer.FullName,
                Email = customer.Email,
                Address = customer.Address,
                PhoneNumber = customer.PhoneNumber
            }
        );
        return await dbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateAsync(Customer customer)
    {
        dbContext.Customers.Update(customer);
        return await dbContext.SaveChangesAsync() > 0;
    }


    public async Task<Customer?> GetByIdAsync(long customerId)
    {
        var customer = await dbContext.Customers.FindAsync(customerId);
        return customer;
    }

    public async Task<ErrorOr<PaginatedResponse<Customer>>> GetAsync(SieveModel sieveModel)
    {
        var query = dbContext.Customers.AsQueryable();

        var totalItems = await sieveProcessor
            .Apply(sieveModel, query, applyPagination: false)
            .CountAsync();

        query = sieveProcessor.Apply(sieveModel, query);

        var result = await query.AsNoTracking().ToListAsync();

        return new PaginatedResponse<Customer>
        {
            Items = result,
            TotalCount = totalItems,
            Page = sieveModel.Page ?? 1,
            PageSize = sieveModel.PageSize ?? 10,
        };
    }
}