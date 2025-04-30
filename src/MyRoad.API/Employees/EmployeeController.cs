using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyRoad.API.Common;
using MyRoad.API.Employees.RequestsDto;
using MyRoad.Domain.Employees;
namespace MyRoad.API.Employees
{
    [Route("api/v{version:apiVersion}/employee")]
    [ApiVersion("1.0")]
    [ApiController]
    public class EmployeeController (IEmployeeService employeeService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEmployeeDto dto)
        {
            var response = await employeeService.CreateAsync(dto.ToDomainEmployee());
            return ResponseHandler.HandleResult(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateEmployeeDto dto)
        {
            var response = await employeeService.UpdateAsync(dto.ToDomainEmployee());
            return ResponseHandler.HandleResult(response);
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id )
        {
            var response = await employeeService.DeleteAsync(id);
            return ResponseHandler.HandleResult(response);
        }

        [HttpPut("restore/{id:long}")]
        public async Task<IActionResult> Restore(long id)
        {
            var result = await employeeService.RestoreAsync(id);
            return ResponseHandler.HandleResult(result);
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById(long id)
        {
            var response = await employeeService.GetByIdAsync(id);
            return ResponseHandler.HandleResult(response);
        }

        [HttpPost("search")]
        public async Task<IActionResult> GetAll([FromBody] RetrievalRequest request)
        {
            var response = await employeeService.GetAsync(request.ToSieveModel());
            return ResponseHandler.HandleResult(response);
        }
    }
}
