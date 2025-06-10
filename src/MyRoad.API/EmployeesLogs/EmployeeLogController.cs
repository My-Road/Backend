using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyRoad.API.Common;
using MyRoad.API.EmployeesLogs.RequestDto;
using MyRoad.Domain.EmployeesLogs;
using MyRoad.Domain.Reports;

namespace MyRoad.API.EmployeesLogs
{
    [Route("api/v{version:apiVersion}/employeelog")]
    [ApiVersion("1.0")]
    [ApiController]
    public class EmployeeLogController(
        IEmployeeLogService employeeLogService,
        IReportBuilderEmployeesLogService reportBuilderEmployeesLogService,
        IPdfGeneratorService pdfGeneratorService
    ) : ControllerBase
    {
        [HttpPost]
        [Authorize(Policy = AuthorizationPolicies.FactoryOwnerOrAdminOrManager)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateEmployeeLogDto dto)
        {
            var response = await employeeLogService.CreateAsync(dto.ToDomainEmployeeLog());
            return ResponseHandler.HandleResult(response);
        }

        [HttpDelete("{id:long}")]
        [Authorize(Policy = AuthorizationPolicies.FactoryOwnerOrAdmin)]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            var response = await employeeLogService.DeleteAsync(id);
            return ResponseHandler.HandleResult(response);
        }

        [HttpPost("search")]
        [Authorize(Policy = AuthorizationPolicies.FactoryOwnerOrAdminOrManager)]
        public async Task<IActionResult> Get([FromBody] RetrievalRequest request)
        {
            var response = await employeeLogService.GetAsync(request.ToSieveModel());
            
            return ResponseHandler.HandleResult(
                response.ToContractPaginatedList(EmployeeLogMapper.ToSearchResponseDto)
            );
        }


        [HttpPut]
        [Authorize(Policy = AuthorizationPolicies.FactoryOwnerOrAdmin)]
        public async Task<IActionResult> Update([FromBody] UpdateEmployeeLogDto dto)
        {
            var response = await employeeLogService.UpdateAsync(dto.ToDomainEmployeeLog());
            return ResponseHandler.HandleResult(response);
        }

        [HttpGet("{id:long}")]
        [Authorize(Policy = AuthorizationPolicies.FactoryOwnerOrAdminOrManager)]
        public async Task<IActionResult> GetById(long id)
        {
            var response = await employeeLogService.GetByIdAsync(id);
            return ResponseHandler.HandleResult(
                response.ToContract(EmployeeLogMapper.ToSearchResponseDto)
            );
        }

        [HttpPost("by-employee/{employeeId:long}")]
        [Authorize(Policy = AuthorizationPolicies.FactoryOwnerOrAdmin)]
        public async Task<IActionResult> GetByEmployeeId(long employeeId, [FromBody] RetrievalRequest request)
        {
            var response = await employeeLogService.GetByEmployeeIdAsync(employeeId, request.ToSieveModel());
            return ResponseHandler.HandleResult(
                response.ToContractPaginatedList(EmployeeLogMapper.ToDomainEmployeeLogResponseDto)
            );
        }
    }
}