using ErrorOr;
using MyRoad.Domain.Common.Entities;
using MyRoad.Domain.Reports;
using Sieve.Models;
using ErrorOr;
using MyRoad.Domain.Dashboard;

namespace MyRoad.Domain.Orders;

public interface IOrderRepository
{
    Task<bool> CreateAsync(Order order);

    Task<bool> UpdateAsync(Order order);

    Task<Order?> GetByIdAsync(long id);

    Task<PaginatedResponse<Order>> GetAsync(SieveModel sieveModel);
    
    Task<PaginatedResponse<Order>>GetByCustomerAsync(long customerId, SieveModel sieveModel);


    Task<List<Order>> GetOrdersForReportAsync(SieveModel sieveModel);

    Task<decimal> GetTotalIncomeAsync(DateOnly? from = null);

}