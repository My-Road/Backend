using MyRoad.Domain.Common.Entities;
using MyRoad.Domain.Reports;
using Sieve.Models;

namespace MyRoad.Domain.EmployeesLogs
{
    public interface IEmployeeLogRepository
    {
        Task<bool> CreateAsync(EmployeeLog employeeLog);

        Task<bool> UpdateAsync(EmployeeLog employeeLog);

        Task<EmployeeLog?> GetByIdAsync(long id);

        Task<PaginatedResponse<EmployeeLog>> GetByEmployeeAsync(long employeeId, SieveModel sieveModel);

        Task<PaginatedResponse<EmployeeLog>> GetAsync(SieveModel sieveModel);

        Task<IEnumerable<EmployeeLog>> GetLogsByDateAsync(long employeeId, DateOnly date);


        Task<List<EmployeeLog>> GetEmployeesLogForReportAsync(SieveModel sieveModel);

        Task<decimal> GetTotalExpensesAsync(DateOnly? from = null);

    }
}