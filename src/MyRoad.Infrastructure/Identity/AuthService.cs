using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using MyRoad.Domain.Identity.Interfaces;
using MyRoad.Domain.Identity.RequestsDto;
using MyRoad.Domain.Identity.Responses;
using MyRoad.Domain.Users;
using MyRoad.Infrastructure.Identity.Entities;

namespace MyRoad.Infrastructure.Identity;

public class AuthService(UserManager<ApplicationUser> userManager, IJwtTokenGenerator tokenGenerator)
    : IAuthService
{
    public async Task<User?> AuthenticateAsync(LoginRequestDto dto)
    {
        var userApplication = userManager.FindByEmailAsync(dto.Email).Result;
        if (userApplication is null || !await userManager.CheckPasswordAsync(userApplication, dto.Password))
        {
            return null;
        }

        return new User()
        {
            Id = userApplication.Id,
            Email = userApplication.Email,
            Username = userApplication.UserName,
            Role = userApplication.Role
        };
    }
}