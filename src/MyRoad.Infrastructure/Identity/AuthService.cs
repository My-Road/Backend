using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using MyRoad.Domain.Identity.Interfaces;
using MyRoad.Domain.Identity.RequestsDto;
using MyRoad.Domain.Users;
using MyRoad.Infrastructure.Identity.Entities;

namespace MyRoad.Infrastructure.Identity;

public class AuthService(UserManager<ApplicationUser> userManager, ILogger<AuthService> logger)
    : IAuthService
{
    public async Task<User?> AuthenticateAsync(LoginRequestDto dto)
    {
        var userApplication = await userManager.FindByEmailAsync(dto.Email);
        if (userApplication is null)
            return null;

        var isPasswordValid = await userManager.CheckPasswordAsync(userApplication, dto.Password);
        if (!isPasswordValid)
            return null;

        return new User
        {
            Id = userApplication.Id,
            Email = userApplication.Email,
            Username = userApplication.UserName,
            Role = userApplication.Role
        };
    }

    public async Task<string?> RegisterUser(RegisterRequestDto registerRequestDto, string password)
    {
        var userApplication = await userManager.FindByEmailAsync(registerRequestDto.Email);
        if (userApplication is not null)
            return "user already exists";

        var user = new ApplicationUser
        {
            UserName = registerRequestDto.Email,
            Email = registerRequestDto.Email,
            PhoneNumber = registerRequestDto.PhoneNumber,
            FirstName = registerRequestDto.FirstName,
            LastName = registerRequestDto.LastName,
            Role = registerRequestDto.Role
        };

        var result = await userManager.CreateAsync(user, password);

        if (result.Succeeded) return string.Empty;

        var errors = result.Errors.Select(e => e.Description);
        var errorMessage = string.Join(", ", errors);

        logger.LogWarning("User registration failed: {Errors}", errorMessage);

        return errorMessage;
    }
}