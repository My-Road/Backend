
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
            var validate = await _employeeValidator.ValidateAsync(employee);
            if (!validate.IsValid)
            {
                return validate.ExtractErrors();
            }

            var isCreated = await employeeRepository.CreateAsync(employee);

            return isCreated ? new Success() : new ErrorOr<Success>();
        }

        public async Task<ErrorOr<Success>> UpdateAsync(Employee employee)
        {
            var validate = await _employeeValidator.ValidateAsync(employee);
            if (!validate.IsValid)
                return validate.ExtractErrors();

            var result = await employeeRepository.GetByIdAsync(employee.Id);
            if (result is null || result.IsDeleted)
                return EmployeeErrors.NotFound;

            result.FullName = employee.FullName;
            result.JobTitle = employee.JobTitle;
            result.PhoneNumber = employee.PhoneNumber;
            result.Address = employee.Address;
            result.Notes = employee.Notes;
            result.Status = employee.Status;
            result.StartDate = employee.StartDate;
            result.EndDate = employee.EndDate;

            var isUpdated = await employeeRepository.UpdateAsync(result);
            return isUpdated.IsError ? isUpdated.Errors : new Success();
        }

        public async Task<ErrorOr<Success>> DeleteAsync(long id)
        {
            var employee = await employeeRepository.GetByIdAsync(id);
            if (employee is null)
                return EmployeeErrors.NotFound;

            var result = employee.Delete();
            if(result.IsError)
                return result.Errors;

            await employeeRepository.UpdateAsync(employee);
            return new Success();
        }

        public async Task<ErrorOr<Employee>> GetByIdAsync(long id)
        {
            var employee = await employeeRepository.GetByIdAsync(id);
            return employee is null||employee.IsDeleted ? EmployeeErrors.NotFound : employee;
        }

        public async Task<ErrorOr<PaginatedResponse<Employee>>> GetAsync(SieveModel sieveModel)
        {
            var employees = await employeeRepository.GetAsync(sieveModel);
            return employees;
        }

        public async Task<ErrorOr<Success>> RestoreAsync(long id)
        {
            var employee = await employeeRepository.GetByIdAsync(id);
            if(employee is null)
                return EmployeeErrors.NotFound;

            var result = employee.Restore();
            if(result.IsError)
                return result.Errors;

            await employeeRepository.UpdateAsync(employee);
            return new Success();
        }
    }
}
