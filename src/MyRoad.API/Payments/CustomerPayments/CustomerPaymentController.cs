using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using MyRoad.API.Common;
using MyRoad.API.Payments.CustomerPayments.RequestDto;
using MyRoad.Domain.Payments.CustomerPayments;

namespace MyRoad.API.Payments.CustomerPayments;

[Route("api/v{version:apiVersion}/customer-payment")]
[ApiVersion("1.0")]
[ApiController]
public class CustomerPaymentController(
    ICustomerPaymentService customerPaymentService
) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateCustomerPaymentDto dto)
    {
        var response = await customerPaymentService.CreateAsync(dto.ToCustomerPayment());
        return ResponseHandler.HandleResult(response);
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> DeleteAsync(long id, [FromBody] string note)
    {
        var response = await customerPaymentService.DeleteAsync(id, note);
        return ResponseHandler.HandleResult(response);
    }

    [HttpPost("search")]
    public async Task<IActionResult> Get([FromBody] RetrievalRequest request)
    {
        var response = await customerPaymentService.GetAsync(request.ToSieveModel());
        return ResponseHandler.HandleResult(response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateCustomerPaymentDto dto)
    {
        var response = await customerPaymentService.UpdateAsync(dto.ToCustomerPayment());
        return ResponseHandler.HandleResult(response);
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id)
    {
        var response = await customerPaymentService.GetByIdAsync(id);
        return ResponseHandler.HandleResult(response);
    }

    [HttpPost("by-customer/{customerId:long}")]
    public async Task<IActionResult> GetByCustomerId(long customerId, [FromBody] RetrievalRequest request)
    {
        var response = await customerPaymentService.GetByCustomerIdAsync(customerId, request.ToSieveModel());
        return ResponseHandler.HandleResult(response);
    }
}