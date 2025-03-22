using Microsoft.AspNetCore.Identity;
using MyRoad.Domain.Identity.Interfaces;
using MyRoad.Domain.Identity.RequestsDto;
using MyRoad.Domain.Users;
using MyRoad.Infrastructure.Identity.Entities;
using ErrorOr;
using MyRoad.Domain.Identity;

namespace MyRoad.Infrastructure.Identity;

public class AuthService(UserManager<ApplicationUser> userManager)
    : IAuthService
{
    public async Task<ErrorOr<User>> AuthenticateAsync(LoginRequestDto dto)
    {
        var userApplication = await userManager.FindByEmailAsync(dto.Email);
        if (userApplication is null)
            return UserErrors.InvalidCredentials;

        var isPasswordValid = await userManager.CheckPasswordAsync(userApplication, dto.Password);
        if (!isPasswordValid)
            return UserErrors.InvalidCredentials;

        return new User
        {
            Id = userApplication.Id,
            Email = userApplication.Email,
            Username = userApplication.UserName,
            Role = userApplication.Role
        };
    }

    public async Task<ErrorOr<bool>> RegisterUser(RegisterRequestDto registerRequestDto, string password)
    {
        var userApplication = await userManager.FindByEmailAsync(registerRequestDto.Email);
        if (userApplication is not null)
            return UserErrors.EmailExists;

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

        if (result.Succeeded)
            return true;


        var errors = result.Errors
            .Select(e => IdentityErrors.GenericError(e.Description))
            .ToList();

        return errors;
    }
}