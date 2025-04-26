using MyRoad.Domain.Employees;
using MyRoad.Infrastructure.Persistence;
using ErrorOr;
using MyRoad.Domain.Common.Entities;
using Sieve.Models;
using Sieve.Services;
using Microsoft.EntityFrameworkCore;

namespace MyRoad.Infrastructure.Employees;

public class EmployeeRepository(
    AppDbContext dbContext,
    ISieveProcessor sieveProcessor
) : IEmployeeRepository
{
    public async Task<ErrorOr<PaginatedResponse<Employee>>> GetAsync(SieveModel sieveModel)
    {
        var query = dbContext.Employee.AsQueryable();

        var totalItems = await sieveProcessor
            .Apply(sieveModel, query, applyPagination: false)
            .CountAsync();

        query = sieveProcessor.Apply(sieveModel, query);

        var result = await query.AsNoTracking().ToListAsync();

        return new PaginatedResponse<Employee>
        {
            Items = result,
            TotalCount = totalItems,
            Page = sieveModel.Page ?? 1,
            PageSize = sieveModel.PageSize ?? 10,
        };
    }

    public async Task<Employee?> GetByIdAsync(long id)
    {
        return await dbContext.Employee
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<ErrorOr<bool>> UpdateAsync(Employee employee)
    {
        var existingEmployee = await dbContext.Employee.FindAsync(employee.Id);

        if (existingEmployee is null)
        {
            return EmployeeErrors.NotFound;
        }

        dbContext.Entry(existingEmployee).CurrentValues.SetValues(employee);
        await dbContext.SaveChangesAsync();
        return true;
    }
}