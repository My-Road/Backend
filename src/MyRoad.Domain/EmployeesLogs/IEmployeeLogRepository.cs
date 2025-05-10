using MyRoad.Domain.Common.Entities;
using Sieve.Models;

namespace MyRoad.Domain.EmployeesLogs
{
    public interface IEmployeeLogRepository
    {
        Task<bool> CreateAsync(EmployeeLog employeelog);
        Task<bool> UpdateAsync(EmployeeLog employeelog);
        Task<EmployeeLog?> GetByIdAsync(long id);
        Task<PaginatedResponse<EmployeeLog>> GetByEmployeeAsync(long employeeId, SieveModel sieveModel);
        Task<PaginatedResponse<EmployeeLog>> GetAsync(SieveModel sieveModel);

    }
}
