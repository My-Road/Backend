using Microsoft.AspNetCore.Identity;
using MyRoad.Domain.Identity.Interfaces;
using MyRoad.Domain.Identity.RequestsDto;
using MyRoad.Domain.Users;
using MyRoad.Infrastructure.Identity.Entities;
using ErrorOr;
using Microsoft.EntityFrameworkCore;
using MyRoad.Domain.Identity;
using static System.Int64;

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

    public async Task<ErrorOr<bool>> ChangePasswordAsync(string userId, string currentPassword, string newPassword)
    {
        TryParse(userId, out var parsedUserId);
        var user = await userManager.Users.FirstOrDefaultAsync(u => u.Id == parsedUserId);

        var result = await userManager.ChangePasswordAsync(user!, currentPassword, newPassword);

        if (result.Succeeded)
        {
            return true;
        }

        var errors = result.Errors
            .Select(e => IdentityErrors.GenericError(e.Description))
            .ToList();

        return errors;
    }

    public async Task<bool> IsOwnPasswordAsync(string userId, string password)
    {
        TryParse(userId, out var parsedUserId);
        var user = await userManager.Users.FirstOrDefaultAsync(u => u.Id == parsedUserId);

        return await userManager.CheckPasswordAsync(user!, password);
    }

    public async Task<string> GenerateResetPasswordTokenAsync(long userId)
    {
        var user = await userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
        return await userManager.GeneratePasswordResetTokenAsync(user!);
    }

    public async Task<bool> ValidateResetPasswordToken(long userId, string token)
    {
        var user = await userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
        return await userManager.VerifyUserTokenAsync(
            user!,
            userManager.Options.Tokens.PasswordResetTokenProvider,
            "ResetPassword",
            token
        );
    }

    public async Task<bool> ResetPasswordAsync(long userId, string token, string newPassword)
    {
        var user = await userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);

        var resetPasswordResult = await userManager.ResetPasswordAsync(user!, token, newPassword);

        return resetPasswordResult.Succeeded;
    }
}