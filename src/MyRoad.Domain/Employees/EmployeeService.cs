using ErrorOr;
using MyRoad.Domain.Common.Entities;
using MyRoad.Domain.Common;
using Sieve.Models;

namespace MyRoad.Domain.Employees
{
    public class EmployeeService(
        IEmployeeRepository employeeRepository
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

            var result = await employeeRepository.FindByPhoneNumberAsync(employee.PhoneNumber);
            if (result is not null)
            {
                return EmployeeErrors.PhoneNumberAlreadyExists;
            }

            employee.TotalDueAmount = Math.Round(employee.TotalDueAmount, 2);

            var isCreated = await employeeRepository.CreateAsync(employee);

            return isCreated ? new Success() : EmployeeErrors.CreationFailed;
        }

        public async Task<ErrorOr<Success>> UpdateAsync(Employee employee)
        {
            var validate = await _employeeValidator.ValidateAsync(employee);
            if (!validate.IsValid)
                return validate.ExtractErrors();

            var result = await employeeRepository.FindByPhoneNumberAsync(employee.PhoneNumber);
            if (result is not null && result.Id != employee.Id)
            {
                return EmployeeErrors.PhoneNumberAlreadyExists;
            }

            result = await employeeRepository.GetByIdAsync(employee.Id);
            if (result is null || !result.IsAcitve)
                return EmployeeErrors.NotFound;

            employee.TotalDueAmount = Math.Round(employee.TotalDueAmount, 2);

            result.MapUpdatedEmployee(employee);
            await employeeRepository.UpdateAsync(result);
            return new Success();
        }

        public async Task<ErrorOr<Success>> DeleteAsync(long id)
        {
            var employee = await employeeRepository.GetByIdAsync(id);
            if (employee is null)
                return EmployeeErrors.NotFound;

            var result = employee.Delete();
            if (result.IsError)
                return result.Errors;

            await employeeRepository.UpdateAsync(employee);
            return new Success();
        }

        public async Task<ErrorOr<Employee>> GetByIdAsync(long id)
        {
            var employee = await employeeRepository.GetByIdAsync(id);
            return (employee is null || !employee.IsAcitve) ? EmployeeErrors.NotFound : employee;
        }

        public async Task<ErrorOr<PaginatedResponse<Employee>>> GetAsync(SieveModel sieveModel)
        {
            var employees = await employeeRepository.GetAsync(sieveModel);
            return employees;
        }

        public async Task<ErrorOr<Success>> RestoreAsync(long id)
        {
            var employee = await employeeRepository.GetByIdAsync(id);
            if (employee is null)
                return EmployeeErrors.NotFound;

            var result = employee.Restore();
            if (result.IsError)
                return result.Errors;

            await employeeRepository.UpdateAsync(employee);
            return new Success();
        }
    }
}