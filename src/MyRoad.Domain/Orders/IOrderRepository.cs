using MyRoad.Domain.Common.Entities;
using Sieve.Models;
using ErrorOr;

namespace MyRoad.Domain.Orders;

public interface IOrderRepository
{
    Task<bool> CreateAsync(Order order);

    Task<bool> UpdateAsync(Order order);

    Task<Order?> GetByIdAsync(long id);

    Task<PaginatedResponse<Order>> GetAsync(SieveModel sieveModel);
    Task<PaginatedResponse<Order>>GetByCustomerAsync(long customerId, SieveModel sieveModel);
}