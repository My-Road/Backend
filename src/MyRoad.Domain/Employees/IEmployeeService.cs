using ErrorOr;
using MyRoad.Domain.Common.Entities;
using Sieve.Models;

namespace MyRoad.Domain.Employees
{
    public interface IEmployeeService
    {
        Task<ErrorOr<Success>> CreateAsync(Employee employee);
        Task<ErrorOr<Success>> UpdateAsync(Employee employee);
        Task<ErrorOr<Success>> DeleteAsync(long id);
        Task<ErrorOr<Employee>> GetByIdAsync(long id);
        Task<ErrorOr<PaginatedResponse<Employee>>> GetAsync(SieveModel sieveModel);
        Task<ErrorOr<Success>> RestoreAsync(long id);
    }
}
