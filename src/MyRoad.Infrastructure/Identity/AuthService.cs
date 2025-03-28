using Microsoft.AspNetCore.Identity;
using MyRoad.Domain.Identity.Interfaces;
using MyRoad.Domain.Users;
using MyRoad.Infrastructure.Identity.Entities;
using ErrorOr;
using Microsoft.EntityFrameworkCore;
using MyRoad.Domain.Identity;

namespace MyRoad.Infrastructure.Identity;

public class AuthService(UserManager<ApplicationUser> userManager)
    : IAuthService
{
    public async Task<ErrorOr<User>> AuthenticateAsync(string email ,string password)
    {
        var userApplication = await userManager.FindByEmailAsync(email);
        if (userApplication is null)
            return UserErrors.InvalidCredentials;

        var isPasswordValid = await userManager.CheckPasswordAsync(userApplication, password);
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

    public async Task<ErrorOr<bool>> RegisterUser(User user, string password)
    {
        var userApplication = await userManager.FindByEmailAsync(user.Email);
        if (userApplication is not null)
            return UserErrors.EmailExists;

        var applicationUser = new ApplicationUser
        {
            UserName = user.Email,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Role = user.Role
        };

        var result = await userManager.CreateAsync(applicationUser, password);

        if (result.Succeeded)
            return true;


        var errors = result.Errors
            .Select(e => IdentityErrors.GenericError(e.Description))
            .ToList();

        return errors;
    }

    public async Task<ErrorOr<bool>> ChangePasswordAsync(long userId, string currentPassword, string newPassword)
    {
        var user = await userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);

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

    public async Task<bool> IsOwnPasswordAsync(long userId, string password)
    {
        var user = await userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);

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