using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyRoad.API.Common;
using MyRoad.Domain.Dashboard;

namespace MyRoad.API.Dashboard;

[Route("api/v{version:apiVersion}/dashboard")]
[ApiVersion("1.0")]
[ApiController]
public class DashboardController(IDashboardOverviewService dashboardOverviewService) : ControllerBase
{
   [HttpGet]
   [Authorize(Policy = AuthorizationPolicies.FactoryOwnerOrAdmin)]
   public async Task<IActionResult> GetDashboardOverviewAsync()
   {
      var response = await dashboardOverviewService.ExecuteAsync();
      return ResponseHandler.HandleResult(
         response.ToContract(DashboardMapper.ToDashboardOverviewDto)
      );
   }
}