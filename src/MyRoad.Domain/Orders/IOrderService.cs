using ErrorOr;
using MyRoad.Domain.Common.Entities;
using Sieve.Models;

namespace MyRoad.Domain.Orders;

public interface IOrderService
{
    
    Task<ErrorOr<Success>> CreateAsync(Order order);
    
    Task<ErrorOr<Success>> UpdateAsync(Order order);
    
    Task<ErrorOr<Success>> DeleteAsync(long id, string note);
    
    Task<ErrorOr<Success>> RestoreAsync(long customerId, string note);
    
    Task<ErrorOr<Order>> GetByIdAsync(long id);
    
    Task<ErrorOr<PaginatedResponse<Order>>> GetByCustomerIdAsync(long customerId, SieveModel sieveModel);
   
    Task<ErrorOr<PaginatedResponse<Order>>> GetAsync(SieveModel sieveModel);
}