
using ErrorOr;
using MyRoad.Domain.Common.Entities;
using MyRoad.Domain.Common;
using MyRoad.Domain.Identity.Interfaces;
using Sieve.Models;

namespace MyRoad.Domain.Employees
{
    public class EmployeeService(
IEmployeeRepository employeeRepository,
IUnitOfWork unitOfWork
) : IEmployeeService
    {
        private readonly EmployeeValidator _employeeValidator = new();

        public async Task<ErrorOr<Success>> CreateAsync(Employee employee)
        {
            var validation = await _employeeValidator.ValidateAsync(employee);
            if (!validation.IsValid)
                return validation.ExtractErrors();

            await unitOfWork.BeginTransactionAsync();
            var result = await employeeRepository.UpdateAsync(employee);
            if (result.IsError)
            {
                await unitOfWork.RollbackTransactionAsync();
                return result.Errors;
            }

            await unitOfWork.CommitTransactionAsync();
            return new Success();
        }

        public async Task<ErrorOr<Success>> UpdateAsync(Employee employee)
        {
            var validation = await _employeeValidator.ValidateAsync(employee);
            if (!validation.IsValid)
                return validation.ExtractErrors();

            var existing = await employeeRepository.GetByIdAsync(employee.Id);
            if (existing is null)
                return EmployeeErrors.NotFound;

            existing.FullName = employee.FullName;
            existing.JobTitle = employee.JobTitle;
            existing.PhoneNumber = employee.PhoneNumber;
            existing.Address = employee.Address;
            existing.Notes = employee.Notes;
            existing.Status = employee.Status;
            existing.TotalDueAmount = employee.TotalDueAmount;
            existing.TotalPaidAmount = employee.TotalPaidAmount;
            existing.StartDate = employee.StartDate;
            existing.EndDate = employee.EndDate;

            var updateResult = await employeeRepository.UpdateAsync(existing);
            return updateResult.IsError ? updateResult.Errors : new Success();
        }

        public async Task<ErrorOr<Success>> DeleteAsync(long id,string note)
        {
            var employee = await employeeRepository.GetByIdAsync(id);
            if (employee is null)
                return EmployeeErrors.NotFound;

            employee.Status = false;
            employee.EndDate = DateTime.UtcNow;
            employee.Notes= note; 

            var updateResult = await employeeRepository.UpdateAsync(employee);
            return updateResult.IsError ? updateResult.Errors : new Success();
        }

        public async Task<ErrorOr<Employee>> GetByIdAsync(long id)
        {
            var employee = await employeeRepository.GetByIdAsync(id);
            return employee is null ? EmployeeErrors.NotFound : employee;
        }

        public async Task<ErrorOr<PaginatedResponse<Employee>>> GetAsync(SieveModel sieveModel)
        {
            var employees = await employeeRepository.GetAsync(sieveModel);
            return employees;
        }
    }
}
