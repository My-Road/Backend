using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyRoad.API.Common;
using MyRoad.API.Orders.RequestDto;
using MyRoad.Domain.Orders;

namespace MyRoad.API.Orders;

[Route("api/v{version:apiVersion}/order")]
[ApiVersion("1.0")]
[ApiController]
public class OrderController(
    IOrderService orderService
) : ControllerBase
{
    [HttpPost]
    [Authorize(Policy = AuthorizationPolicies.FactoryOwnerOrAdminOrManager)]
    public async Task<IActionResult> CreateAsync([FromBody] CreateOrderDto dto)
    {
        var response = await orderService.CreateAsync(dto.ToDomainOrder());
        return ResponseHandler.HandleResult(response);
    }

    [HttpDelete("{id:long}")]
    [Authorize(Policy = AuthorizationPolicies.FactoryOwnerOrAdmin)]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        var response = await orderService.DeleteAsync(id);
        return ResponseHandler.HandleResult(response);
    }

    [HttpPost("search")]
    [Authorize(Policy = AuthorizationPolicies.FactoryOwnerOrAdminOrManager)]
    public async Task<IActionResult> Get([FromBody] RetrievalRequest request)
    {
        var response = await orderService.GetAsync(request.ToSieveModel());

        return ResponseHandler.HandleResult(
            response.ToContractPaginatedList(OrderMapper.ToSearchResponseDto)
        );
    }

    [HttpPut]
    [Authorize(Policy = AuthorizationPolicies.FactoryOwnerOrAdmin)]
    public async Task<IActionResult> Update([FromBody] UpdateOrderDto dto)
    {
        var response = await orderService.UpdateAsync(dto.ToDomainOrder());
        return ResponseHandler.HandleResult(response);
    }

    [HttpGet("{id:long}")]
    [Authorize(Policy = AuthorizationPolicies.FactoryOwnerOrAdmin)]
    public async Task<IActionResult> GetById(long id)
    {
        var response = await orderService.GetByIdAsync(id);
        return ResponseHandler.HandleResult(
            response.ToContract(OrderMapper.ToDomainOrderResponseDto)
        );
    }

    [HttpPost("by-customer/{customerId:long}")]
    [Authorize(Policy = AuthorizationPolicies.FactoryOwnerOrAdmin)]
    public async Task<IActionResult> GetByCustomerId(long customerId, [FromBody] RetrievalRequest request)
    {
        var response = await orderService.GetByCustomerIdAsync(customerId, request.ToSieveModel());
        return ResponseHandler.HandleResult(
            response.ToContractPaginatedList(OrderMapper.ToDomainOrderResponseDto)
        );
    }
}