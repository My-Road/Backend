using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyRoad.API.Common;
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

        return ResponseHandler.HandleResult(response);
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

        return ResponseHandler.HandleResult(response);
    }

    [Authorize]
    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePassword(ChangePasswordRequestDto dto)
    {
        var response = await identityService.ChangePassword(dto.CurrentPassword, dto.NewPassword);
        return ResponseHandler.HandleResult(response);
    }

    [HttpPost("forget-password")]
    public async Task<IActionResult> ForgetPassword(ForgetPasswordRequestDto dto)
    {
        var response = await identityService.ForgotPassword(dto.Email);
        return ResponseHandler.HandleResult(response);
        ;
    }

    [HttpPost("reset-forget-password")]
    public async Task<IActionResult> ResetForgetPassword(ResetForgetPasswordRequestDto dto)
    {
        var response =
            await identityService.ResetForgetPassword(dto.UserId, dto.Token, dto.NewPassword, dto.ConfirmNewPassword);
        return ResponseHandler.HandleResult(response);
    }
}