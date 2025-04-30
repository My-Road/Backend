using ErrorOr;
using MyRoad.Domain.Common.Entities;
using Sieve.Models;
namespace MyRoad.Domain.Employees;

public interface IEmployeeRepository
{
    Task <bool> CreateAsync(Employee employee);
    Task<ErrorOr<bool>> UpdateAsync(Employee employee);
    Task<Employee?> GetByIdAsync(long id);
    Task<ErrorOr<PaginatedResponse<Employee>>> GetAsync(SieveModel sieveModel);

}