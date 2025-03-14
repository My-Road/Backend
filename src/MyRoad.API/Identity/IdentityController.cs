using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
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
}