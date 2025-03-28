using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyRoad.API.Extensions;
using MyRoad.API.Identity.RequestsDto;
using MyRoad.Domain.Identity.Interfaces;

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

        var response = await identityService.Login(loginRequestDto.Email, loginRequestDto.Password);

        return response.IsError ? response.ToProblemDetails() : Ok(response.Value);
    }

    [Authorize(Policy = "SuperAdmin")]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var response = await identityService.Register(dto.ToDomainUser());

        return response.IsError ? response.ToProblemDetails() : Ok(response.Value);
    }

    [Authorize]
    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePassword(ChangePasswordRequestDto dto)
    {
        var response = await identityService.ChangePassword(dto.CurrentPassword, dto.NewPassword);
        return response.IsError ? response.ToProblemDetails() : Ok(response.Value);
    }

    [HttpPost("forget-password")]
    public async Task<IActionResult> ForgetPassword(ForgetPasswordRequestDto dto)
    {
        var response = await identityService.ForgotPassword(dto.Email);
        return response.IsError ? response.ToProblemDetails() : Ok(response.Value);
    }

    [HttpPost("reset-forget-password")]
    public async Task<IActionResult> ResetForgetPassword(ResetForgetPasswordRequestDto dto)
    {
        var response = await identityService.ResetForgetPassword(dto.Token, dto.NewPassword, dto.ConfirmNewPassword);
        return response.IsError ? response.ToProblemDetails() : Ok(response.Value);
    }
}