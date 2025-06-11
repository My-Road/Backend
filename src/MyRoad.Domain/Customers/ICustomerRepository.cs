using ErrorOr;
using MyRoad.Domain.Common.Entities;
using Sieve.Models;

namespace MyRoad.Domain.Customers;

public interface ICustomerRepository
{
    Task<bool> CreateAsync(Customer customer);

    Task<bool> UpdateAsync(Customer customer);

    Task<Customer?> GetByIdAsync(long customerId);

    Task<ErrorOr<PaginatedResponse<Customer>>> GetAsync(SieveModel sieveModel);
    
    Task<Customer?> FindByPhoneNumber(string? customerPhoneNumber);
    Task<Customer?> FindByEmail(string? customerEmail);
    
    Task<long> CountAsync();
}