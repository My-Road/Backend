using ErrorOr;
using MyRoad.Domain.Common;
using MyRoad.Domain.Common.Entities;
using MyRoad.Domain.Customers;
using MyRoad.Domain.Identity.Enums;
using MyRoad.Domain.Identity.Interfaces;
using MyRoad.Domain.Users;
using Sieve.Models;

namespace MyRoad.Domain.Orders;

public class OrderService(
    IUserContext userContext,
    IOrderRepository orderRepository,
    ICustomerRepository customerRepository,
    IUserRepository userRepository,
    IUnitOfWork unitOfWork) : IOrderService
{
    private readonly OrderValidator _orderValidator = new();

    public async Task<ErrorOr<Success>> CreateAsync(Order order)
    {
        var result = await _orderValidator.ValidateAsync(order);
        if (!result.IsValid)
        {
            return result.ExtractErrors();
        }

        var customer = await customerRepository.GetByIdAsync(order.CustomerId);
        if (customer is null)
        {
            return CustomerErrors.NotFound;
        }

        var user = await userRepository.GetByIdAsync(order.CreatedByUserId);
        if (user is null)
        {
            return UserErrors.NotFound;
        }

        await unitOfWork.BeginTransactionAsync();
        try
        {
            switch (user.Role)
            {
                case UserRole.Admin when userContext.Role == UserRole.Admin:
                    customer.TotalDueAmount += order.TotalDueAmount;
                    order.IsCompleted = true;
                    break;
                case UserRole.Manager when userContext.Role == UserRole.Manager:
                    order.IsCompleted = false;
                    break;
                default:
                    return UserErrors.UnauthorizedUser;
            }

            await orderRepository.CreateAsync(order);
            await customerRepository.UpdateAsync(customer);
            await unitOfWork.CommitTransactionAsync();

            return new Success();
        }
        catch (Exception)
        {
            await unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }

    public async Task<ErrorOr<Success>> UpdateAsync(Order order)
    {
        var validationResult = await _orderValidator.ValidateAsync(order);
        if (!validationResult.IsValid)
        {
            return validationResult.ExtractErrors();
        }

        var existingOrder = await orderRepository.GetByIdAsync(order.Id);
        if (existingOrder is null || existingOrder.IsDeleted)
        {
            return OrderErrors.NotFound;
        }

        var customer = await customerRepository.GetByIdAsync(order.CustomerId);
        if (customer is null)
        {
            return CustomerErrors.NotFound;
        }

        if (order.Price <= 0)
        {
            return OrderErrors.InvalidPrice;
        }
        
        var newTotalDueAmount = customer.TotalDueAmount - existingOrder.TotalDueAmount + order.TotalDueAmount;
        if (customer.TotalPaidAmount > newTotalDueAmount)
        {
            return CustomerErrors.CannotUpdateOrder;
        }

        try
        {
            await unitOfWork.BeginTransactionAsync();

            customer.TotalDueAmount = newTotalDueAmount;
            existingOrder.MapUpdatedOrder(order);

            await customerRepository.UpdateAsync(customer);
            await orderRepository.UpdateAsync(existingOrder);
            await unitOfWork.CommitTransactionAsync();

            return new Success();
        }
        catch (Exception)
        {
            await unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }



    public async Task<ErrorOr<Success>> DeleteAsync(long orderId)
    {
        var order = await orderRepository.GetByIdAsync(orderId);
        if (order is null || order.IsDeleted)
        {
            return OrderErrors.NotFound;
        }

        var customer = await customerRepository.GetByIdAsync(order.CustomerId);
        if (customer is null)
        {
            return CustomerErrors.NotFound;
        }

        if (customer.RemainingAmount == 0 || customer.TotalDueAmount - order.TotalDueAmount < customer.TotalPaidAmount)
        {
            return CustomerErrors.CannotRemoveOrder;
        }

        var result = order.Delete();
        if (result.IsError)
        {
            return result.Errors;
        }

        customer.TotalDueAmount -= order.TotalDueAmount;
        await orderRepository.UpdateAsync(order);
        await customerRepository.UpdateAsync(customer);

        return new Success();
    }
    

    public async Task<ErrorOr<Order>> GetByIdAsync(long id)
    {
        var order = await orderRepository.GetByIdAsync(id);
        if (order is null)
        {
            return OrderErrors.NotFound;
        }

        return order;
    }

    public async Task<ErrorOr<PaginatedResponse<Order>>> GetByCustomerIdAsync(long customerId, SieveModel sieveModel)
    {
        var result = await orderRepository.GetByCustomerAsync(customerId, sieveModel);
        return result;
    }

    public async Task<ErrorOr<PaginatedResponse<Order>>> GetAsync(SieveModel sieveModel)
    {
        var result = await orderRepository.GetAsync(sieveModel);

        return result;
    }
}