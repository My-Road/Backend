using ErrorOr;
using MyRoad.Domain.Common;
using MyRoad.Domain.Common.Entities;
using Sieve.Models;

namespace MyRoad.Domain.Customers;

public class CustomerService(
    ICustomerRepository customerRepository
) : ICustomerService
{
    private readonly CustomerValidator _customerValidator = new();

    public async Task<ErrorOr<Success>> CreateAsync(Customer customer)
    {
        var validate = await _customerValidator.ValidateAsync(customer);
        if (!validate.IsValid)
        {
            return validate.ExtractErrors();
        }

        var isCreated = await customerRepository.CreateAsync(customer);

        return isCreated ? new Success() : new ErrorOr<Success>();
    }

    public async Task<ErrorOr<Success>> UpdateAsync(Customer customer)
    {
        var validate = await _customerValidator.ValidateAsync(customer);
        if (!validate.IsValid)
        {
            return validate.ExtractErrors();
        }

        var result = await customerRepository.GetByIdAsync(customer.Id);
        if (result is null || result.IsDeleted)
        {
            return CustomerErrors.NotFound;
        }

        result.MapUpdatedCustomer(customer);
        await customerRepository.UpdateAsync(result);
        return new Success();
    }


    public async Task<ErrorOr<Success>> DeleteAsync(long id)
    {
        var customer = await customerRepository.GetByIdAsync(id);
        if (customer is null)
        {
            return CustomerErrors.NotFound;
        }

        var result = customer.Delete();
        if (result.IsError)
        {
            return result.Errors;
        }

        await customerRepository.UpdateAsync(customer);

        return new Success();
    }

    public async Task<ErrorOr<Customer>> GetByIdAsync(long id)
    {
        var customer = await customerRepository.GetByIdAsync(id);
        if (customer is null || customer.IsDeleted)
        {
            return CustomerErrors.NotFound;
        }

        return customer;
    }

    public async Task<ErrorOr<PaginatedResponse<Customer>>> GetAsync(SieveModel sieveModel)
    {
        var result = await customerRepository.GetAsync(sieveModel);
        return result;
    }

    public async Task<ErrorOr<Success>> RestoreAsync(long id)
    {
        var customer = await customerRepository.GetByIdAsync(id);
        if (customer is null)
        {
            return CustomerErrors.NotFound;
        }

        var result = customer.Restore();
        if (result.IsError)
        {
            return result.Errors;
        }

        await customerRepository.UpdateAsync(customer);
        return new Success();
    }
}