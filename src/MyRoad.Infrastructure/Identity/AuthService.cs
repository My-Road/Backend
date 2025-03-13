using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using MyRoad.Domain.Identity.RequestsDto;
using MyRoad.Domain.Identity.Responses;
using MyRoad.Domain.Identity.Services;
using MyRoad.Domain.Users;
using MyRoad.Infrastructure.Identity.Entities;

namespace MyRoad.Infrastructure.Identity;

public class AuthService(UserManager<ApplicationUser> userManager, JwtTokenGenerator tokenGenerator)
    : IAuthService
{
    public async Task<LoginResponseDto> Login(LoginRequestDto dto)
    {
        var loginResponseDto = new LoginResponseDto();
        var userApplication = userManager.FindByEmailAsync(dto.Email).Result;
        if (userApplication is null || !await userManager.CheckPasswordAsync(userApplication, dto.Password))
        {
            loginResponseDto.Message = "Email or Password is incorrect!";
            loginResponseDto.IsAuthenticated = false;
            return loginResponseDto;
        }

        var user = new User()
        {
            Id = userApplication.Id,
            Email = userApplication.Email,
            Username = userApplication.UserName,
            Role = userApplication.Role,
        };

        var jwtSecurityToken = await tokenGenerator.CreateJwtToken(user);
        var roles = await userManager.GetRolesAsync(userApplication);
        loginResponseDto.IsAuthenticated = true;
        loginResponseDto.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        loginResponseDto.ExpiresOn = jwtSecurityToken.ValidTo;
        loginResponseDto.Role = roles.ToList();

        return loginResponseDto;
    }
}