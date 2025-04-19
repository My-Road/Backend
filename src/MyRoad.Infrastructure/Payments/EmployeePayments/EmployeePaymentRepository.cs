using ErrorOr;
using Microsoft.EntityFrameworkCore;
using MyRoad.Domain.Common.Entities;
using MyRoad.Domain.Payments;
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
        dbContext.EmployeePayment.Add(new EmployeePayment()
        {
            EmployeeId = employeePayment.EmployeeId,
            Amount = employeePayment.Amount,
            PaymentDate = employeePayment.PaymentDate,
            Notes = employeePayment.Notes,
        });
        return await dbContext.SaveChangesAsync() > 0;
    }

    public async Task<ErrorOr<Success>> UpdateAsync(EmployeePayment employeePayment)
    {
        var payment = await dbContext.EmployeePayment.FindAsync(employeePayment.Id);

        if (payment is null)
        {
            return PaymentErrors.NotFound;
        }

        dbContext.EmployeePayment.Update(employeePayment);
        await dbContext.SaveChangesAsync();
        return new Success();
    }


    public async Task<EmployeePayment?> GetByIdAsync(long employeePaymentId)
    {
        return await dbContext.EmployeePayment
            .FirstOrDefaultAsync(p => p.Id == employeePaymentId);
    }

    public async Task<PaginatedResponse<EmployeePayment>> GetAsync(SieveModel sieveModel)
    {
        var query = dbContext.EmployeePayment.AsQueryable();

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
        var query = dbContext.EmployeePayment
            .Where(p => p.EmployeeId == employeeId && !p.IsDeleted)
            .AsQueryable();

        var totalItems = await sieveProcessor
            .Apply(sieveModel, query, applyPagination: false)
            .CountAsync();

        query = sieveProcessor.Apply(sieveModel, query);

        var result = await query.AsNoTracking().ToListAsync();

        var page = sieveModel.Page ?? 1;
        var pageSize = sieveModel.PageSize ?? 10;

        return new PaginatedResponse<EmployeePayment>
        {
            Items = result,
            TotalCount = totalItems,
            Page = sieveModel.Page ?? 1,
            PageSize = sieveModel.PageSize ?? 10,
        };
    }

}