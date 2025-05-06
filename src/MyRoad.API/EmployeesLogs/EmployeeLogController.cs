using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using MyRoad.API.Common;
using MyRoad.API.EmployeesLogs.RequestDto;
using MyRoad.Domain.EmployeesLogs;

namespace MyRoad.API.EmployeesLogs
{
    [Route("api/v{version:apiVersion}/employeelog")]
    [ApiVersion("1.0")]
    [ApiController]
    public class EmployeeLogController(
        IEmployeeLogService employeeLogService
        ) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateEmployeeLogDto dto)
        {
            var response = await employeeLogService.CreateAsync(dto.ToDomainEmployeeLog());
            return ResponseHandler.HandleResult(response);
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            var response = await employeeLogService.DeleteAsync(id);
            return ResponseHandler.HandleResult(response);
        }

        [HttpPost("search")]
        public async Task<IActionResult> Get([FromBody] RetrievalRequest request)
        {
            var response = await employeeLogService.GetAsync(request.ToSieveModel());

            return ResponseHandler.HandleResult(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateEmployeeLogDto dto)
        {
            var response = await employeeLogService.UpdateAsync(dto.ToDomainEmployeeLog());
            return ResponseHandler.HandleResult(response);
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById(long id)
        {
            var response = await employeeLogService.GetByIdAsync(id);
            return ResponseHandler.HandleResult(response);
        }

        [HttpPost("by-employee/{employeeId:long}")]
        public async Task<IActionResult> GetByEmployeeId(long employeeId, [FromBody] RetrievalRequest request)
        {
            var response = await employeeLogService.GetByEmployeeIdAsync(employeeId, request.ToSieveModel());
            return ResponseHandler.HandleResult(response);
        }

    }
}

