using ErrorOr;
using MyRoad.Domain.Common.Entities;
using Sieve.Models;

namespace MyRoad.Domain.EmployeesLogs
{
    public interface IEmployeeLogService
    {
        Task<ErrorOr<Success>> CreateAsync(EmployeeLog employeeLog);
        
        Task<ErrorOr<Success>> UpdateAsync(EmployeeLog employeeLog);
        
        Task<ErrorOr<Success>> DeleteAsync(long id);
        
        Task<ErrorOr<EmployeeLog>> GetByIdAsync(long id);
        
        Task<ErrorOr<PaginatedResponse<EmployeeLog>>> GetByEmployeeIdAsync(long employeeId, SieveModel sieveModel);
        
        Task<ErrorOr<PaginatedResponse<EmployeeLog>>> GetAsync(SieveModel sieveModel);
    }
}
