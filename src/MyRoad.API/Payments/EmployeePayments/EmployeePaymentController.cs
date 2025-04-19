using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using MyRoad.API.Common;
using MyRoad.API.Payments.EmployeePayments.RequestsDto;
using MyRoad.Domain.Employees;
using MyRoad.Domain.Payments.EmployeePayments;

namespace MyRoad.API.Payments.EmployeePayments;

[Route("api/v{version:apiVersion}/EmployeePayments")]
[ApiVersion("1.0")]
[ApiController]
public class EmployeePaymentController(IEmployeePaymentService employeePaymentService, IEmployeeRepository ee)
    : Controller
{
    [HttpPost("Create")]
    public async Task<IActionResult> Create([FromBody] CreateEmployeePaymentDto dto)
    {
        var response = await employeePaymentService.CreateAsync(dto.ToDomainEmployeePayment());

        return ResponseHandler.HandleResult(response);
    }

    [HttpDelete("Delete")]
    public async Task<IActionResult> Delete([FromBody] DeleteEmployeePaymentDto dto)
    {
        var response = await employeePaymentService.DeleteAsync(dto.EmployeePaymentId, dto.note);

        return ResponseHandler.HandleResult(response);
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id)
    {
        var response = await employeePaymentService.GetByIdAsync(id);
        return ResponseHandler.HandleResult(response);
    }

    [HttpGet("search")]
    public async Task<IActionResult> Get([FromQuery] RetrievalRequest request)
    {
        var response = await employeePaymentService.GetAsync(request.ToSieveModel());

        return ResponseHandler.HandleResult(response);
    }

    [HttpPost("Update")]
    public async Task<IActionResult> Update([FromBody] UpdateEmployeePaymentDto dto)
    {
        var response = await employeePaymentService.UpdateAsync(dto.ToDomainEmployeePayment());
        return ResponseHandler.HandleResult(response);
    }

    [HttpGet("GetByEmployeeId/{id:long}")]
    public async Task<IActionResult> GetByEmployeeIdAsync(long id, [FromQuery] RetrievalRequest request)
    {
        var response = await employeePaymentService.GetByEmployeeIdAsync(id, request.ToSieveModel());
        return ResponseHandler.HandleResult(response);
    }
    
}