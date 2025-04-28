using MyRoad.Domain.Employees;
using MyRoad.Infrastructure.Persistence;
using ErrorOr;

namespace MyRoad.Infrastructure.Employees;

public class EmployeeRepository(AppDbContext context) : IEmployeeRepository
{
    public async Task<Employee?> GetByIdAsync(long id)
    {
        return await context.Employees.FindAsync(id);
    }

    public async Task<ErrorOr<bool>> UpdateAsync(Employee employee)
    {
        context.Employees.Update(employee);
        var result = await context.SaveChangesAsync();

        return result > 0;
    }
}