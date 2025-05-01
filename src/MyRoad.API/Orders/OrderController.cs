using Asp.Versioning;
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
    public async Task<IActionResult> CreateAsync([FromBody] CreateOrderDto dto)
    {
        var response = await orderService.CreateAsync(dto.ToDomainOrder());
        return ResponseHandler.HandleResult(response);
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        var response = await orderService.DeleteAsync(id);
        return ResponseHandler.HandleResult(response);
    }

    [HttpPut("restore/{id:long}")]
    public async Task<IActionResult> Restore(long id)
    {
        var result = await orderService.RestoreAsync(id);
        return ResponseHandler.HandleResult(result);
    }

    [HttpPost("search")]
    public async Task<IActionResult> Get([FromBody] RetrievalRequest request)
    {
        var response = await orderService.GetAsync(request.ToSieveModel());

        return ResponseHandler.HandleResult(response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateOrderDto dto)
    {
        var response = await orderService.UpdateAsync(dto.ToDomainOrder());
        return ResponseHandler.HandleResult(response);
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id)
    {
        var response = await orderService.GetByIdAsync(id);
        return ResponseHandler.HandleResult(response);
    }

    [HttpPost("by-customer/{customerId:long}")]
    public async Task<IActionResult> GetByCustomerId(long customerId, [FromBody] RetrievalRequest request)
    {
        var response = await orderService.GetByCustomerIdAsync(customerId, request.ToSieveModel());
        return ResponseHandler.HandleResult(response);
    }
}