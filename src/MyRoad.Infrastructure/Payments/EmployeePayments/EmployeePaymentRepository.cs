using Microsoft.EntityFrameworkCore;
using MyRoad.Domain.Common.Entities;
using MyRoad.Domain.Payments.EmployeePayments;
using MyRoad.Infrastructure.Persistence;
using Sieve.Models;
using Sieve.Services;

namespace MyRoad.Infrastructure.Payments.EmployeePayments;

public class EmployeePaymentRepository(
    AppDbContext dbContext,
    ISieveProcessor sieveProcessor
) : IEmployeePaymentRepository
{
    public async Task<bool> CreateAsync(EmployeePayment employeePayment)
    {
        dbContext.EmployeePayments.Add(
            employeePayment
        );
        return await dbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateAsync(EmployeePayment employeePayment)
    {
        dbContext.EmployeePayments.Update(employeePayment);

        return await dbContext.SaveChangesAsync() > 0;
    }


    public async Task<EmployeePayment?> GetByIdAsync(long employeePaymentId)
    {
        return await dbContext.EmployeePayments
            .FirstOrDefaultAsync(p => p.Id == employeePaymentId);
    }

    public async Task<PaginatedResponse<EmployeePayment>> GetAsync(SieveModel sieveModel)
    {
        var query = dbContext.EmployeePayments
            .Include(o => o.Employee)
            .AsQueryable();

        var totalItems = await sieveProcessor
            .Apply(sieveModel, query, applyPagination: false)
            .CountAsync();

        query = sieveProcessor.Apply(sieveModel, query);

        var result = await query.AsNoTracking().ToListAsync();

        return new PaginatedResponse<EmployeePayment>
        {
            Items = result,
            TotalCount = totalItems,
            Page = sieveModel.Page ?? 1,
            PageSize = sieveModel.PageSize ?? 10,
        };
    }

    public async Task<PaginatedResponse<EmployeePayment>> GetByEmployeeIdAsync(long employeeId, SieveModel sieveModel)
    {
        var query = dbContext.EmployeePayments
            .Where(p => p.EmployeeId == employeeId && !p.IsDeleted)
            .AsQueryable();

        var totalItems = await sieveProcessor
            .Apply(sieveModel, query, applyPagination: false)
            .CountAsync();

        query = sieveProcessor.Apply(sieveModel, query);

        var result = await query.AsNoTracking().ToListAsync();

        return new PaginatedResponse<EmployeePayment>
        {
            Items = result,
            TotalCount = totalItems,
            Page = sieveModel.Page ?? 1,
            PageSize = sieveModel.PageSize ?? 10,
        };
    }
}