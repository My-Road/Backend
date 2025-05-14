using ErrorOr;
using FluentValidation;
using MyRoad.Domain.Common;
using MyRoad.Domain.Common.Entities;
using MyRoad.Domain.Customers;
using MyRoad.Domain.Identity.Interfaces;
using Sieve.Models;

namespace MyRoad.Domain.Payments.CustomerPayments;

public class CustomerPaymentService(
    ICustomerPaymentRepository customerPaymentRepository,
    ICustomerRepository customerRepository,
    IUnitOfWork unitOfWork
) : ICustomerPaymentService
{
    private readonly PaymentValidator _paymentValidator = new();
    private readonly CustomerPaymentValidator _customerValidator = new();

    public async Task<ErrorOr<Success>> CreateAsync(CustomerPayment customerPayment)
    {
        var validators = new List<IValidator<CustomerPayment>>
        {
            _paymentValidator,
            _customerValidator
        };

        foreach (var validator in validators)
        {
            var result = await validator.ValidateAsync(customerPayment);
            if (!result.IsValid)
            {
                return result.ExtractErrors();
            }
        }

        var customer = await customerRepository.GetByIdAsync(customerPayment.CustomerId);
        if (customer is null)
        {
            return CustomerErrors.NotFound;
        }

        if (customer.TotalPaidAmount >= customer.TotalDueAmount )
        {
            return PaymentErrors.NoDueAmountLeft;
        }

        if (customer.TotalPaidAmount + customerPayment.Amount > customer.TotalDueAmount)
        {
            return PaymentErrors.PaymentExceedsDue;
        }

        customer.TotalPaidAmount += customerPayment.Amount;
        try
        {
            await unitOfWork.BeginTransactionAsync();
            var updateResult = await customerRepository.UpdateAsync(customer);
            if (!updateResult)
            {
                await unitOfWork.RollbackTransactionAsync();
                throw new Exception("Internal Server Error");
            }

            if (await customerPaymentRepository.CreateAsync(customerPayment))
            {
                await unitOfWork.CommitTransactionAsync();
                return new Success();
            }
        }
        catch (Exception)
        {
            await unitOfWork.RollbackTransactionAsync();
            throw;
        }

        return new Success();
    }

    public async Task<ErrorOr<Success>> UpdateAsync(CustomerPayment customerPayment)
    {
        var validators = new List<IValidator<CustomerPayment>>
        {
            _paymentValidator,
            _customerValidator
        };

        foreach (var validator in validators)
        {
            var result = await validator.ValidateAsync(customerPayment);
            if (!result.IsValid)
            {
                return result.ExtractErrors();
            }
        }

        var existingPayment = await customerPaymentRepository.GetByIdAsync(customerPayment.Id);
        if (existingPayment is null || existingPayment.IsDeleted)
        {
            return PaymentErrors.NotFound;
        }

        var customer = await customerRepository.GetByIdAsync(customerPayment.CustomerId);
        if (customer is null)
        {
            return CustomerErrors.NotFound;
        }

        try
        {
            await unitOfWork.BeginTransactionAsync();
            var newTotalPaidAmount = customer.TotalPaidAmount - existingPayment.Amount + customerPayment.Amount;
            if (newTotalPaidAmount > customer.TotalDueAmount)
            {
                return PaymentErrors.UpdatedPaymentExceedsDue;
            }

            customer.TotalPaidAmount = newTotalPaidAmount;

            existingPayment.MapUpdatedCustomerPayment(customerPayment);

            await customerRepository.UpdateAsync(customer);
            await customerPaymentRepository.UpdateAsync(existingPayment);

            await unitOfWork.CommitTransactionAsync();

            return new Success();
        }
        catch (Exception)
        {
            await unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }

    public async Task<ErrorOr<Success>> DeleteAsync(long id)
    {
        var payment = await customerPaymentRepository.GetByIdAsync(id);

        if (payment is null || payment.IsDeleted)
        {
            return PaymentErrors.NotFound;
        }

        var employee = await customerRepository.GetByIdAsync(payment.CustomerId);
        if (employee is null)
        {
            return CustomerErrors.NotFound;
        }

        employee.TotalPaidAmount -= payment.Amount;

        var result = payment.Delete();

        if (result.IsError)
        {
            return result.Errors;
        }

        await customerRepository.UpdateAsync(employee);

        return new Success();
    }

    public async Task<ErrorOr<CustomerPayment>> GetByIdAsync(long id)
    {
        var payment = await customerPaymentRepository.GetByIdAsync(id);
        if (payment is null)
        {
            return PaymentErrors.NotFound;
        }

        return payment;
    }

    public async Task<ErrorOr<PaginatedResponse<CustomerPayment>>> GetByCustomerIdAsync(long customerId,
        SieveModel sieveModel)
    {
        var result = await customerPaymentRepository.GetByCustomerAsync(customerId, sieveModel);
        return result;
    }

    public async Task<ErrorOr<PaginatedResponse<CustomerPayment>>> GetAsync(SieveModel sieveModel)
    {
        var result = await customerPaymentRepository.GetAsync(sieveModel);

        return result;
    }
}