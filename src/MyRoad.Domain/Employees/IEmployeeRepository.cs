using ErrorOr;
namespace MyRoad.Domain.Employees;

public interface IEmployeeRepository
{
    Task<Employee?> GetByIdAsync(long id);
    Task<ErrorOr<bool>> UpdateAsync(Employee employee);
}