using MyRoad.Domain.Common.Entities;
using Sieve.Models;

namespace MyRoad.Domain.Payments.CustomerPayments;

public interface ICustomerPaymentRepository
{
    Task<bool> CreateAsync(CustomerPayment customerPayment);

    Task<bool> UpdateAsync(CustomerPayment customerPayment);

    Task<CustomerPayment?> GetByIdAsync(long id);

    Task<PaginatedResponse<CustomerPayment>> GetAsync(SieveModel sieveModel);

    Task<PaginatedResponse<CustomerPayment>> GetByCustomerAsync(long id, SieveModel sieveModel);
    
    Task<decimal> GetTotalPaymentAsync(DateOnly? from = null);
}