using ErrorOr;
using FluentValidation;
using MyRoad.Domain.Common;
using MyRoad.Domain.Common.Entities;
using MyRoad.Domain.Employees;
using MyRoad.Domain.Identity.Interfaces;
using Sieve.Models;

namespace MyRoad.Domain.Payments.EmployeePayments;

public class EmployeePaymentService(
    IEmployeePaymentRepository employeePaymentRepository,
    IEmployeeRepository employeeRepository,
    IUnitOfWork unitOfWork)
    : IEmployeePaymentService
{
    private readonly PaymentValidator _paymentValidator = new();
    private readonly EmployeePaymentValidator _employeePayments = new();

    public async Task<ErrorOr<Success>> CreateAsync(EmployeePayment employeePayment)
    {
        var validators = new List<IValidator<EmployeePayment>>
        {
            _paymentValidator,
            _employeePayments
        };

        foreach (var validator in validators)
        {
            var result = await validator.ValidateAsync(employeePayment);
            if (!result.IsValid)
            {
                return result.ExtractErrors();
            }
        }

        var employee = await employeeRepository.GetByIdAsync(employeePayment.EmployeeId);
        if (employee is null)
        {
            return EmployeeErrors.NotFound;
        }


        if (employee.TotalPaidAmount + employeePayment.Amount > employee.TotalDueAmount)
        {
            return PaymentErrors.InvalidAmount;
        }

        employee.TotalPaidAmount += employeePayment.Amount;

        try
        {
            await unitOfWork.BeginTransactionAsync();
            var updateResult = await employeeRepository.UpdateAsync(employee);
            if (updateResult.IsError)
            {
                await unitOfWork.RollbackTransactionAsync();
                return updateResult.Errors;
            }

            if (await employeePaymentRepository.CreateAsync(employeePayment))
            {
                await unitOfWork.CommitTransactionAsync();
                return new Success();
            }
        }
        catch (Exception e)
        {
            await unitOfWork.RollbackTransactionAsync();
            throw;
        }
        return new Success();
    }


    public async Task<ErrorOr<Success>> UpdateAsync(EmployeePayment employeePayment)
    {
        var validators = new List<IValidator<EmployeePayment>>
        {
            _paymentValidator,
            _employeePayments
        };

        foreach (var validator in validators)
        {
            var result = await validator.ValidateAsync(employeePayment);
            if (!result.IsValid)
            {
                return result.ExtractErrors();
            }
        }

        var existingPayment = await employeePaymentRepository.GetByIdAsync(employeePayment.Id);
        if (existingPayment is null || existingPayment.IsDeleted)
        {
            return PaymentErrors.NotFound;
        }

        var employee = await employeeRepository.GetByIdAsync(employeePayment.EmployeeId);
        if (employee is null)
        {
            return EmployeeErrors.NotFound;
        }


        try
        {
            await unitOfWork.BeginTransactionAsync();
            var newTotalPaidAmount = employee.TotalPaidAmount - existingPayment.Amount + employeePayment.Amount;
            if (newTotalPaidAmount > employee.TotalDueAmount)
            {
                return PaymentErrors.InvalidAmount;
            }


            employee.TotalPaidAmount = newTotalPaidAmount;

            existingPayment.Amount = employeePayment.Amount;
            existingPayment.PaymentDate = employeePayment.PaymentDate;
            existingPayment.Notes = employeePayment.Notes;


            await employeeRepository.UpdateAsync(employee);
            await employeePaymentRepository.UpdateAsync(existingPayment);

            await unitOfWork.CommitTransactionAsync();

            return new Success();
        }
        catch (Exception)
        {
            await unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }


    public async Task<ErrorOr<Success>> DeleteAsync(long id, string note)
    {
        var payment = await employeePaymentRepository.GetByIdAsync(id);

        if (payment is null || payment.IsDeleted)
        {
            return PaymentErrors.NotFound;
        }

        var employee = await employeeRepository.GetByIdAsync(payment.EmployeeId);
        if (employee is null)
        {
            return EmployeeErrors.NotFound;
        }

        employee.TotalPaidAmount -= payment.Amount;
        payment.IsDeleted = true;
        payment.DeletedAt = DateTime.UtcNow;
        payment.Notes = note;

        var updateEmployeeResult = await employeeRepository.UpdateAsync(employee);
        if (updateEmployeeResult.IsError)
        {
            return updateEmployeeResult.Errors;
        }

        return new Success();
    }


    public async Task<ErrorOr<EmployeePayment>> GetByIdAsync(long id)
    {
        var payment = await employeePaymentRepository.GetByIdAsync(id);
        if (payment is null)
        {
            return PaymentErrors.NotFound;
        }

        return payment;
    }

    public async Task<ErrorOr<PaginatedResponse<EmployeePayment>>> GetByEmployeeIdAsync(long employeeId,
        SieveModel sieveModel)
    {
        var result = await employeePaymentRepository.GetByEmployeeIdAsync(employeeId, sieveModel);
        return result;
    }

    public async Task<ErrorOr<PaginatedResponse<EmployeePayment>>> GetAsync(SieveModel sieveModel)
    {
        var result = await employeePaymentRepository.GetAsync(sieveModel);

        return result;
    }
}