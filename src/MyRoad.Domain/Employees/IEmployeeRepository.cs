using ErrorOr;
using MyRoad.Domain.Common.Entities;
using Sieve.Models;
namespace MyRoad.Domain.Employees;

public interface IEmployeeRepository
{
    Task<ErrorOr<PaginatedResponse<Employee>>> GetAsync(SieveModel sieveModel);
    Task<Employee?> GetByIdAsync(long id);
    Task<ErrorOr<bool>> UpdateAsync(Employee employee);
}