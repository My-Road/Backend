using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyRoad.API.Common;
using MyRoad.Domain.Dashboard;
using MyRoad.Domain.Identity.Enums;
using MyRoad.Domain.Identity.Interfaces;

namespace MyRoad.API.Dashboard;

[Route("api/v{version:apiVersion}/dashboard")]
[ApiVersion("1.0")]
[ApiController]
public class DashboardController(
    IDashboardOverviewService dashboardOverviewService,
    IUserContext userContext) : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = AuthorizationPolicies.FactoryOwnerOrAdminOrManager)]
    public async Task<IActionResult> GetDashboardOverviewAsync()
    {
        return userContext.Role switch
        {
            UserRole.Manager =>
                ResponseHandler.HandleResult(
                    (await dashboardOverviewService.ExecuteManagerAsync())
                    .ToContract(DashboardMapper.ToDashboardManagerOverviewDto)
                ),

            UserRole.Admin or UserRole.FactoryOwner =>
                ResponseHandler.HandleResult(
                    (await dashboardOverviewService.ExecuteAdminAsync())
                    .ToContract(DashboardMapper.ToDashboardAdminOverviewDto)
                ),

            _ => Unauthorized()
        };
    }
}