using System.Data;
using ErrorOr;
using MyRoad.Domain.Common;
using MyRoad.Domain.Common.Entities;
using MyRoad.Domain.Employees;
using MyRoad.Domain.Identity.Enums;
using MyRoad.Domain.Identity.Interfaces;
using MyRoad.Domain.Users;
using Sieve.Models;

namespace MyRoad.Domain.EmployeesLogs
{
    public class EmployeeLogService(
        IUserContext userContext,
        IEmployeeLogRepository employeeLogRepository,
        IEmployeeRepository employeeRepository,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        ITimeOverlapValidator timeOverlapValidator
    ) : IEmployeeLogService
    {
        private readonly EmployeeLogValidator _employeeLogValidator = new();

        public async Task<ErrorOr<Success>> CreateAsync(EmployeeLog employeeLog)
        {
            var result = await _employeeLogValidator.ValidateAsync(employeeLog);
            if (!result.IsValid)
            {
                return result.ExtractErrors();
            }

            var employee = await employeeRepository.GetByIdAsync(employeeLog.EmployeeId);
            if (employee == null)
            {
                return EmployeeErrors.NotFound;
            }

            var user = await userRepository.GetByIdAsync(employeeLog.CreatedByUserId);
            if (user is null)
            {
                return UserErrors.NotFound;
            }

            await unitOfWork.BeginTransactionAsync(IsolationLevel.Serializable);

            try
            {
                var employeeLogs = await employeeLogRepository
                    .GetLogsByDateAsync(employee.Id, employeeLog.Date);

                var hasOverLap = timeOverlapValidator.HasOverlapAsync(employeeLog, employeeLogs);

                if (hasOverLap)
                {
                    await unitOfWork.RollbackTransactionAsync();
                    return EmployeeLogErrors.TimeOverlapError;
                }

                switch (user.Role)
                {
                    case UserRole.Admin when userContext.Role == UserRole.Admin:
                        employee.TotalDueAmount += employeeLog.DailyWage;
                        employeeLog.IsCompleted = true;
                        break;
                    case UserRole.Manager when userContext.Role == UserRole.Manager:
                        employeeLog.IsCompleted = false;
                        break;
                    default:
                        await unitOfWork.RollbackTransactionAsync();
                        return UserErrors.UnauthorizedUser;
                }

                await employeeLogRepository.CreateAsync(employeeLog);
                await employeeRepository.UpdateAsync(employee);
                await unitOfWork.CommitTransactionAsync();

                return new Success();
            }
            catch
            {
                await unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<ErrorOr<Success>> UpdateAsync(EmployeeLog employeeLog)
        {
            var validationResult = await _employeeLogValidator.ValidateAsync(employeeLog);
            if (!validationResult.IsValid)
            {
                return validationResult.ExtractErrors();
            }

            var existingEmployeeLog = await employeeLogRepository.GetByIdAsync(employeeLog.Id);
            if (existingEmployeeLog is null || existingEmployeeLog.IsDeleted)
            {
                return EmployeeLogErrors.NotFound;
            }

            var employee = await employeeRepository.GetByIdAsync(employeeLog.EmployeeId);
            if (employee is null || !employee.IsActive)
            {
                return EmployeeErrors.NotFound;
            }

            await unitOfWork.BeginTransactionAsync(IsolationLevel.Serializable);

            try
            {
                var employeeLogs = await employeeLogRepository
                    .GetLogsByDateAsync(employee.Id, employeeLog.Date);

                var hasOverLap = timeOverlapValidator.HasOverlapAsync(employeeLog, employeeLogs);
                if (hasOverLap)
                {
                    await unitOfWork.RollbackTransactionAsync();
                    return EmployeeLogErrors.TimeOverlapError;
                }

                var prevDailyWage = existingEmployeeLog.DailyWage;
                var newDailyWage = employeeLog.DailyWage;
                var newTotalDueAmount = employee.TotalDueAmount - prevDailyWage + newDailyWage;

                if (employee.TotalPaidAmount > newTotalDueAmount)
                {
                    await unitOfWork.RollbackTransactionAsync();
                    return EmployeeLogErrors.InvalidWageUpdate;
                }

                employee.TotalDueAmount = newTotalDueAmount;
                existingEmployeeLog.MapUpdateEmployeeLog(employeeLog);

                await employeeRepository.UpdateAsync(employee);
                await employeeLogRepository.UpdateAsync(existingEmployeeLog);
                await unitOfWork.CommitTransactionAsync();

                return new Success();
            }
            catch
            {
                await unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }


        public async Task<ErrorOr<Success>> DeleteAsync(long id)
        {
            var employeeLog = await employeeLogRepository.GetByIdAsync(id);
            if (employeeLog is null || employeeLog.IsDeleted)
            {
                return EmployeeLogErrors.NotFound;
            }

            var employee = await employeeRepository.GetByIdAsync(employeeLog.EmployeeId);
            if (employee is null || !employee.IsActive)
            {
                return EmployeeErrors.NotFound;
            }

            if (employee.RemainingAmount == 0 ||
                employee.TotalDueAmount - employeeLog.DailyWage < employee.TotalPaidAmount)
            {
                return EmployeeErrors.CannotRemoveEmployeeLog;
            }

            var result = employeeLog.Delete();
            if (result.IsError)
            {
                return result.Errors;
            }

            employee.TotalDueAmount -= employeeLog.DailyWage;
            await employeeLogRepository.UpdateAsync(employeeLog);
            await employeeRepository.UpdateAsync(employee);

            return new Success();
        }

        public async Task<ErrorOr<PaginatedResponse<EmployeeLog>>> GetAsync(SieveModel sieveModel)
        {
            var result = await employeeLogRepository.GetAsync(sieveModel);

            return result;
        }

        public async Task<ErrorOr<PaginatedResponse<EmployeeLog>>> GetByEmployeeIdAsync(long employeeId,
            SieveModel sieveModel)
        {
            var result = await employeeLogRepository.GetByEmployeeAsync(employeeId, sieveModel);
            return result;
        }

        public async Task<ErrorOr<EmployeeLog>> GetByIdAsync(long id)
        {
            var employeeLog = await employeeLogRepository.GetByIdAsync(id);
            if (employeeLog is null)
            {
                return EmployeeLogErrors.NotFound;
            }

            return employeeLog;
        }
    }
}