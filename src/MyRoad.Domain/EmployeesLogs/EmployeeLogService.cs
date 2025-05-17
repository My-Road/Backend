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
        IUnitOfWork unitOfWork
    ) : IEmployeeLogService
    {
        private readonly EmployeeLogValidator _employeeLogValidator = new();

        public async Task<ErrorOr<Success>> CreateAsync(EmployeeLog employeelog)
        {
            var result = await _employeeLogValidator.ValidateAsync(employeelog);
            if (!result.IsValid)
            {
                return result.ExtractErrors();
            }

            var employee = await employeeRepository.GetByIdAsync(employeelog.EmployeeId);
            if (employee == null)
            {
                return EmployeeErrors.NotFound;
            }

            var user = await userRepository.GetByIdAsync(employeelog.CreatedByUserId);
            if (user is null)
            {
                return UserErrors.NotFound;
            }

            var employeeLogs = await employeeLogRepository
                .GetLogsByDateAsync(employee.Id, employeelog.Date);

            var hasOverLap = LogsOverlapChecker.HasOverlap(employeelog, employeeLogs);

            if (hasOverLap)
            {
                return EmployeeLogErrors.TimeOverlapError;
            }

            await unitOfWork.BeginTransactionAsync();

            try
            {
                switch (user.Role)
                {
                    case UserRole.Admin when userContext.Role == UserRole.Admin:
                        employee.TotalDueAmount += employeelog.DailyWage;
                        employeelog.IsCompleted = true;
                        break;
                    case UserRole.Manager when userContext.Role == UserRole.Manager:
                        employeelog.IsCompleted = false;
                        break;
                    default:
                        return UserErrors.UnauthorizedUser;
                }

                await employeeLogRepository.CreateAsync(employeelog);
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

        public async Task<ErrorOr<Success>> UpdateAsync(EmployeeLog employeelog)
        {
            var validationResult = await _employeeLogValidator.ValidateAsync(employeelog);
            if (!validationResult.IsValid)
            {
                return validationResult.ExtractErrors();
            }

            var existingEmployeeLog = await employeeLogRepository.GetByIdAsync(employeelog.Id);
            if (existingEmployeeLog is null || existingEmployeeLog.IsDeleted)
            {
                return EmployeeLogErrors.NotFound;
            }

            var employee = await employeeRepository.GetByIdAsync(employeelog.EmployeeId);
            if (employee is null)
            {
                return EmployeeErrors.NotFound;
            }

            var employeeLogs = await employeeLogRepository
                .GetLogsByDateAsync(employee.Id, employeelog.Date);

            var hasOverLap = LogsOverlapChecker.HasOverlap(employeelog, employeeLogs);

            if (hasOverLap)
            {
                return EmployeeLogErrors.TimeOverlapError;
            }

            var prevDailyWage = existingEmployeeLog.DailyWage;
            var newDailyWage = employeelog.DailyWage;
            var newTotalDueAmount = employee.TotalDueAmount - prevDailyWage + newDailyWage;
            if (employee.TotalPaidAmount > newTotalDueAmount)
            {
                return EmployeeLogErrors.InvalidWageUpdate;
            }

            try
            {
                await unitOfWork.BeginTransactionAsync();
                employee.TotalDueAmount = newTotalDueAmount;
                existingEmployeeLog.MapUpdateEmployeeLog(employeelog);

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
            if (employee is null || !employee.Status)
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