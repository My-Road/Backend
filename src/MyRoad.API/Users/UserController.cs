using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyRoad.API.Common;
using MyRoad.Domain.Users;

namespace MyRoad.API.Users;

[Route("api/v{version:apiVersion}/Users")]
[ApiVersion("1.0")]
[ApiController]
public class UserController(IUserService userService)
{
    [HttpPost("search")]
    [Authorize(Policy = AuthorizationPolicies.Admin)]
    public async Task<IActionResult> Get([FromBody] RetrievalRequest request)
    {
        var response = await userService.GetAsync(request.ToSieveModel());

        return ResponseHandler.HandleResult(response);
    }

    [HttpPatch("{id:long}/toggle-status")]
    [Authorize(Policy = AuthorizationPolicies.Admin)]
    public async Task<IActionResult> ToggleStatus(long id)
    {
        var response = await userService.ToggleStatus(id);

        return ResponseHandler.HandleResult(response);
    }
}