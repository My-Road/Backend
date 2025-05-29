using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Policy = AuthorizationPolicies.FactoryOwnerOrAdmin)]
    public async Task<IActionResult> CreateAsync([FromBody] CreateCustomerPaymentDto dto)
    {
        var response = await customerPaymentService.CreateAsync(dto.ToCustomerPayment());
        return ResponseHandler.HandleResult(response);
    }

    [HttpDelete("{id:long}")]
    [Authorize(Policy = AuthorizationPolicies.FactoryOwnerOrAdmin)]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        var response = await customerPaymentService.DeleteAsync(id);
        return ResponseHandler.HandleResult(response);
    }

    [HttpPost("search")]
    [Authorize(Policy = AuthorizationPolicies.FactoryOwnerOrAdmin)]
    public async Task<IActionResult> Get([FromBody] RetrievalRequest request)
    {
        var response = await customerPaymentService.GetAsync(request.ToSieveModel());
        return ResponseHandler.HandleResult(
            response.ToContractPaginatedList(CustomerPaymentMapper.ToCustomerPaymentResponseDto)
        );
    }

    [HttpPut]
    [Authorize(Policy = AuthorizationPolicies.FactoryOwnerOrAdmin)]
    public async Task<IActionResult> Update([FromBody] UpdateCustomerPaymentDto dto)
    {
        var response = await customerPaymentService.UpdateAsync(dto.ToCustomerPayment());
        return ResponseHandler.HandleResult(response);
    }

    [HttpGet("{id:long}")]
    [Authorize(Policy = AuthorizationPolicies.FactoryOwnerOrAdmin)]
    public async Task<IActionResult> GetById(long id)
    {
        var response = await customerPaymentService.GetByIdAsync(id);
        return ResponseHandler.HandleResult(
            response.ToContract(PaymentMapper.ToPaymentResponseDto)
        );
    }

    [HttpPost("by-customer/{customerId:long}")]
    [Authorize(Policy = AuthorizationPolicies.FactoryOwnerOrAdmin)]
    public async Task<IActionResult> GetByCustomerId(long customerId, [FromBody] RetrievalRequest request)
    {
        var response = await customerPaymentService.GetByCustomerIdAsync(customerId, request.ToSieveModel());
        return ResponseHandler.HandleResult(
            response.ToContractPaginatedList(PaymentMapper.ToPaymentResponseDto)
        );
    }
}