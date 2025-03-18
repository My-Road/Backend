using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyRoad.Domain.Identity.Enums;
using MyRoad.Domain.Identity.Interfaces;
using MyRoad.Domain.Identity.RequestsDto;

namespace MyRoad.API.Identity;

[Route("api/v{version:apiVersion}/identity")]
[ApiVersion("1.0")]
[ApiController]
public class IdentityController(IIdentityService identityService) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await identityService.Login(loginRequestDto);

        if (!result.IsAuthenticated)
            return Unauthorized(result.Message);

        return Ok(result);
    }

    [Authorize(Roles = nameof(UserRole.SuperAdmin))]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var response = await identityService.Register(dto);

        if (!response.IsCreated )
        {
            return BadRequest(response);
        }

        return CreatedAtAction(nameof(Register), response);
    }
}