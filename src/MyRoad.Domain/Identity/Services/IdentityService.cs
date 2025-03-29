using ErrorOr;
using Microsoft.Extensions.Logging;
using MyRoad.Domain.Email;
using MyRoad.Domain.Identity.Interfaces;
using MyRoad.Domain.Identity.Validators;
using MyRoad.Domain.Common;
using MyRoad.Domain.Identity.Models;
using MyRoad.Domain.Users;

namespace MyRoad.Domain.Identity.Services;

public class IdentityService(
    IAuthService authService,
    IJwtTokenGenerator tokenGenerator,
    IPasswordGenerationService passwordGenerationService,
    IEmailService emailService,
    IUnitOfWork unitOfWork,
    IUserService userService,
    IUserContext userContext,
    ILogger<IdentityService> logger)
    : IIdentityService
{
    private readonly RegisterValidator _registerValidator = new();
    private readonly ChangePasswordValidator _passwordValidator = new();
    private readonly ForgotPasswordValidator _forgotPasswordValidator = new();

    public async Task<ErrorOr<AccessToken>> Login(string email, string password)
    {
        var user = await authService.AuthenticateAsync(email, password);

        if (user.IsError)
        {
            return user.Errors;
        }

        var accessToken = tokenGenerator.CreateJwtToken(user.Value);

        return accessToken;
    }

    public async Task<ErrorOr<Success>> Register(User user)
    {
        var validationResult = await _registerValidator.ValidateAsync(user);
        if (!validationResult.IsValid)
        {
            return validationResult.ExtractErrors();
        }


        var password = passwordGenerationService.GenerateRandomPassword(8);

        if (password.IsError)
        {
            return password.Errors;
        }

        await unitOfWork.BeginTransactionAsync();
        var result = await authService.RegisterUser(user, password.Value);

        if (result.IsError)
        {
            await unitOfWork.RollbackTransactionAsync();
            return result.Errors;
        }

        var emailResult = await emailService.SendEmailAsync(new EmailRequest()
        {
            ToEmails = [user.Email],
            Subject = "Welcome to MyRoad Company",
            Body = EmailUtils.GetRegistrationEmailBody(
                user.Email,
                $"{user.FirstName} {user.LastName}",
                password.Value,
                "loginLink")
        });

        if (emailResult.IsError)
        {
            await unitOfWork.RollbackTransactionAsync();
            return emailResult.Errors;
        }

        await unitOfWork.CommitTransactionAsync();
        return new Success();
    }

    public async Task<ErrorOr<Success>> ChangePassword(string currentPassword, string newPassword)
    {
        var validationResult =
            await _passwordValidator.ValidateAsync(new ChangePasswordDto(currentPassword, newPassword));
        if (!validationResult.IsValid)
        {
            return validationResult.ExtractErrors();
        }

        var userId = userContext.Id;
        logger.LogInformation("user Id: " + userContext.Id + " Email: " + userContext.Email + " Role: " +
                              userContext.Role);

        var userReturnResult = await userService.GetByIdAsync(userId);
        if (userReturnResult.IsError)
        {
            return UserErrors.NotFound;
        }

        if (!await authService.IsOwnPasswordAsync(userId, currentPassword))
        {
            return IdentityErrors.WrongCurrentPassword;
        }

        if (userReturnResult.Value.Email is null)
        {
            return UserErrors.NotFound;
        }

        await unitOfWork.BeginTransactionAsync();
        var result = await authService.ChangePasswordAsync(userId, currentPassword, newPassword);

        if (result.IsError)
        {
            await unitOfWork.RollbackTransactionAsync();
            return result.Errors;
        }

        var user = userReturnResult.Value;
        var emailResult = await emailService.SendEmailAsync(new EmailRequest()
        {
            ToEmails = [user.Email],
            Subject = "Your password has been changed",
            Body = EmailUtils.GetPasswordChangeSuccessEmailBody(user.Email, user.FirstName + " " + user.LastName),
        });
        if (emailResult.IsError)
        {
            await unitOfWork.RollbackTransactionAsync();
            return emailResult.Errors;
        }

        await unitOfWork.CommitTransactionAsync();
        return new Success();
    }

    public async Task<ErrorOr<Success>> ForgotPassword(string email)
    {
        var userReturnResult = await userService.GetByEmailAsync(email);
        if (userReturnResult.IsError)
        {
            return userReturnResult.Errors;
        }

        var user = userReturnResult.Value;
        if (user.Email is null)
        {
            return UserErrors.NotFound;
        }

        var token = await authService.GenerateResetPasswordTokenAsync(user.Id);

        var emailResult = await emailService.SendEmailAsync(new EmailRequest()
        {
            ToEmails = [user.Email],
            Subject = "Rest your password in MyRoad Company",
            Body = EmailUtils.GetPasswordResetEmailBody(user.Email, user.FirstName + " " + user.LastName, token,
                user.Id.ToString()),
        });
        if (emailResult.IsError)
        {
            return emailResult.Errors;
        }

        return new Success();
    }

    public async Task<ErrorOr<Success>> ResetForgetPassword(long userId, string token, string newPassword,
        string confirmNewPassword)
    {
        var validationResult =
            await _forgotPasswordValidator.ValidateAsync(new ForgetPasswordRequestDto(newPassword, confirmNewPassword));
        if (!validationResult.IsValid)
        {
            return validationResult.ExtractErrors();
        }

        var userReturnResult = await userService.GetByIdAsync(userId);
        if (userReturnResult.IsError)
        {
            return userReturnResult.Errors;
        }

        var user = userReturnResult.Value;
        if (user.Email is null)
        {
            return UserErrors.NotFound;
        }


        if (!await authService.ValidateResetPasswordToken(user.Id, token))
        {
            return IdentityErrors.InvalidResetPasswordToken;
        }

        await unitOfWork.BeginTransactionAsync();

        if (!await authService.ResetPasswordAsync(user.Id, token, newPassword))
        {
            await unitOfWork.RollbackTransactionAsync();
            return IdentityErrors.FailedToResetPassword;
        }

        var emailResult = await emailService.SendEmailAsync(new EmailRequest
        {
            ToEmails = [user.Email],
            Subject = "Your password has been changed",
            Body = EmailUtils.GetPasswordChangedEmailBody($"{user.FirstName}  {user.LastName}")
        });

        if (emailResult.IsError)
        {
            await unitOfWork.RollbackTransactionAsync();
            return emailResult.Errors;
        }

        await unitOfWork.CommitTransactionAsync();

        return new Success();
    }
}