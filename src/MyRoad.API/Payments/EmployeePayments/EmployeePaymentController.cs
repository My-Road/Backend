using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyRoad.API.Common;
using MyRoad.API.Payments.EmployeePayments.RequestsDto;
using MyRoad.Domain.Payments.EmployeePayments;

namespace MyRoad.API.Payments.EmployeePayments;

[Route("api/v{version:apiVersion}/employee-payments")]
[ApiVersion("1.0")]
[ApiController]
public class EmployeePaymentController(IEmployeePaymentService employeePaymentService)
    : ControllerBase
{
    [HttpPost]
    [Authorize(Policy = AuthorizationPolicies.FactoryOwnerOrAdmin)]
    public async Task<IActionResult> Create([FromBody] CreateEmployeePaymentDto dto)
    {
        var response = await employeePaymentService.CreateAsync(dto.ToDomainEmployeePayment());

        return ResponseHandler.HandleResult(response);
    }

    [HttpDelete("{id:long}")]
    [Authorize(Policy = AuthorizationPolicies.FactoryOwnerOrAdmin)]
    public async Task<IActionResult> Delete(long id)
    {
        var response = await employeePaymentService.DeleteAsync(id);

        return ResponseHandler.HandleResult(response);
    }

    [HttpGet("{id:long}")]
    [Authorize(Policy = AuthorizationPolicies.FactoryOwnerOrAdmin)]
    public async Task<IActionResult> GetById(long id)
    {
        var response = await employeePaymentService.GetByIdAsync(id);
        return ResponseHandler.HandleResult(
            response.ToContract(PaymentMapper.ToEmployeePaymentResponseDto)
        );
    }

    [HttpPost("search")]
    [Authorize(Policy = AuthorizationPolicies.FactoryOwnerOrAdmin)]
    public async Task<IActionResult> Get([FromBody] RetrievalRequest request)
    {
        var response = await employeePaymentService.GetAsync(request.ToSieveModel());

        return ResponseHandler.HandleResult(
            response.ToContractPaginatedList(EmployeePaymentMapper.ToEmployeePaymentResponseDto)
        );
    }

    [HttpPut]
    [Authorize(Policy = AuthorizationPolicies.FactoryOwnerOrAdmin)]
    public async Task<IActionResult> Update([FromBody] UpdateEmployeePaymentDto dto)
    {
        var response = await employeePaymentService.UpdateAsync(dto.ToDomainEmployeePayment());
        return ResponseHandler.HandleResult(response);
    }

    [HttpPost("by-employee/{employeeId:long}")]
    [Authorize(Policy = AuthorizationPolicies.FactoryOwnerOrAdmin)]
    public async Task<IActionResult> GetByEmployeeIdAsync(long employeeId, [FromBody] RetrievalRequest request)
    {
        var response = await employeePaymentService.GetByEmployeeIdAsync(employeeId, request.ToSieveModel());
        return ResponseHandler.HandleResult(
            response.ToContractPaginatedList(PaymentMapper.ToPaymentResponseDto)
        );
    }
}