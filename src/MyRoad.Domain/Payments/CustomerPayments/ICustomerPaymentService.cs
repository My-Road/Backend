using ErrorOr;
using MyRoad.Domain.Common.Entities;
using Sieve.Models;

namespace MyRoad.Domain.Payments.CustomerPayments;

public interface ICustomerPaymentService
{
    Task<ErrorOr<Success>> CreateAsync(CustomerPayment customerPayment);

    Task<ErrorOr<Success>> UpdateAsync(CustomerPayment customerPayment);

    Task<ErrorOr<Success>> DeleteAsync(long id, string note);

    Task<ErrorOr<CustomerPayment>> GetByIdAsync(long id);

    Task<ErrorOr<PaginatedResponse<CustomerPayment>>> GetByCustomerIdAsync(long customerId, SieveModel sieveModel);

    Task<ErrorOr<PaginatedResponse<CustomerPayment>>> GetAsync(SieveModel sieveModel);
}