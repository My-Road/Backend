using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyRoad.API.Common;
using MyRoad.API.Users.RequestDto;
using MyRoad.Domain.Identity.Enums;
using MyRoad.Domain.Identity.Interfaces;
using MyRoad.Domain.Users;

namespace MyRoad.API.Users;

[Route("api/v{version:apiVersion}/Users")]
[ApiVersion("1.0")]
[ApiController]
public class UserController(
    IUserService userService,
    IUserContext userContext)
{
    [HttpPost("search")]
    [Authorize(Policy = AuthorizationPolicies.FactoryOwner)]
    public async Task<IActionResult> Get([FromBody] RetrievalRequest request)
    {
        var response = await userService.GetAsync(request.ToSieveModel());

        return ResponseHandler.HandleResult(response);
    }

    [HttpPatch("{id:long}/toggle-status")]
    [Authorize(Policy = AuthorizationPolicies.FactoryOwner)]
    public async Task<IActionResult> ToggleStatus(long id)
    {
        var response = await userService.ToggleStatus(id);

        return ResponseHandler.HandleResult(response);
    }

    [HttpPatch("{id:long}/role")]
    [Authorize(Policy = AuthorizationPolicies.FactoryOwner)]
    public async Task<IActionResult> ChangeRole(long id, UserRole role)
    {
        var response = await userService.ChangeRole(id, role);

        return ResponseHandler.HandleResult(response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateUserProfileDto dto)
    {
        var userId = userContext.Id;
        var response = await userService.UpdateAsync(userId, dto.ToDomainUser());
        return ResponseHandler.HandleResult(response);
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetUser()
    {
        var userId = userContext.Id;
        var response = await userService.GetByIdAsync(userId);
        return ResponseHandler.HandleResult(response.ToContract(UserMapper.ToDomainUser));
    }
}