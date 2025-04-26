using ErrorOr;
using MyRoad.Domain.Common.Entities;
using Sieve.Models;

namespace MyRoad.Domain.Customers;

public interface ICustomerService
{
    Task<ErrorOr<Success>> CreateAsync(Customer customer);

    Task<ErrorOr<Success>> UpdateAsync(Customer customer);

    Task<ErrorOr<Success>> DeleteAsync(long customerId);

    Task<ErrorOr<Customer>> GetByIdAsync(long customerId);

    Task<ErrorOr<PaginatedResponse<Customer>>> GetAsync(SieveModel sieveModel);

    Task<ErrorOr<Success>> RestoreAsync(long customerId);
}