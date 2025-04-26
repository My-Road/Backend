using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyRoad.API.Common;
using MyRoad.API.Employees.RequestsDto;
using MyRoad.Domain.Employees;
namespace MyRoad.API.Employees
{
    [Route("api/[controller]")]
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

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteEmployeeDto dto)
        {
            var response = await employeeService.DeleteAsync(dto.Id, dto.Note);
            return ResponseHandler.HandleResult(response);
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById(long id)
        {
            var response = await employeeService.GetByIdAsync(id);
            return ResponseHandler.HandleResult(response);
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetAll([FromQuery] RetrievalRequest request)
        {
            var response = await employeeService.GetAsync(request.ToSieveModel());
            return ResponseHandler.HandleResult(response);
        }
    }
}
