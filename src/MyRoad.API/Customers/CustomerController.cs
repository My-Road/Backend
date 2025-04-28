using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using MyRoad.API.Common;
using MyRoad.API.Customers.RequestDto;
using MyRoad.Domain.Customers;

namespace MyRoad.API.Customers;

[Route("api/v{version:apiVersion}/customer")]
[ApiVersion("1.0")]
[ApiController]
public class CustomerController(ICustomerService customerService)
    : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCustomerDto dto)
    {
        var response = await customerService.CreateAsync(dto.ToDomainCustomer());
        return ResponseHandler.HandleResult(response);
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id)
    {
        var response = await customerService.DeleteAsync(id);
        return ResponseHandler.HandleResult(response);
    }

    [HttpPut("restore/{id:long}")]
    public async Task<IActionResult> Restore(long id)
    {
        var result = await customerService.RestoreAsync(id);
        return ResponseHandler.HandleResult(result);
    }

    [HttpPost("search")]
    public async Task<IActionResult> Get([FromBody] RetrievalRequest request)
    {
        var response = await customerService.GetAsync(request.ToSieveModel());

        return ResponseHandler.HandleResult(response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateCustomerDto dto)
    {
        var response = await customerService.UpdateAsync(dto.ToDomainCustomer());
        return ResponseHandler.HandleResult(response);
    }


    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id)
    {
        var response = await customerService.GetByIdAsync(id);
        return ResponseHandler.HandleResult(response);
    }
}