using ErrorOr;
using MyRoad.Domain.Email;
using MyRoad.Domain.Identity.Interfaces;
using MyRoad.Domain.Identity.RequestsDto;
using MyRoad.Domain.Identity.Validators;
using MyRoad.Domain.Common;
using MyRoad.Domain.Identity.ResponsesDto;
using MyRoad.Domain.Users;

namespace MyRoad.Domain.Identity.Services;

public class IdentityService(
    IAuthService authService,
    IJwtTokenGenerator tokenGenerator,
    IPasswordGenerationService passwordGenerationService,
    IEmailService emailService,
    IUnitOfWork unitOfWork,
    IUserService userService)
    : IIdentityService
{
    private readonly RegisterValidator _registerValidator = new();
    private readonly ChangePasswordValidator _passwordValidator = new();
    private readonly ForgotPasswordValidator _forgotPasswordValidator = new();

    public async Task<ErrorOr<LoginResponseDto>> Login(LoginRequestDto dto)
    {
        var user = await authService.AuthenticateAsync(dto);

        if (user.IsError)
        {
            return user.Errors;
        }

        var accessToken = tokenGenerator.CreateJwtToken(user.Value);

        return new LoginResponseDto
        {
            IsAuthenticated = true,
            Role = Enum.GetName(user.Value.Role) ?? "UnknownRole",
            Token = accessToken.Token,
            ExpiresOn = accessToken.ExpiresOn
        };
    }

    public async Task<ErrorOr<RegisterResponsesDto>> Register(RegisterRequestDto dto)
    {
        var validationResult = await _registerValidator.ValidateAsync(dto);
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
        var result = await authService.RegisterUser(dto, password.Value);

        if (result.IsError)
        {
            await unitOfWork.RollbackTransactionAsync();
            return result.Errors;
        }

        var emailResult = await emailService.SendEmailAsync(new EmailRequest()
        {
            ToEmails = [dto.Email],
            Subject = "Welcome to MyRoad Company",
            Body = EmailUtils.GetRegistrationEmailBody(
                dto.Email,
                $"{dto.FirstName} {dto.LastName}",
                password.Value,
                "loginLink")
        });

        if (emailResult.IsError)
        {
            await unitOfWork.RollbackTransactionAsync();
            return emailResult.Errors;
        }

        await unitOfWork.CommitTransactionAsync();
        return new RegisterResponsesDto
        {
            Message = "User registered successfully.",
            IsCreated = true,
        };
    }

    public async Task<ErrorOr<Success>> ChangePassword(string userId, ChangePasswordRequestDto dto)
    {
        var validationResult = await _passwordValidator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            return validationResult.ExtractErrors();
        }

        var userReturnResult = await userService.GetByIdAsync(userId);
        if (userReturnResult.IsError)
        {
            return UserErrors.NotFound;
        }

        if (!await authService.IsOwnPasswordAsync(userId, dto.CurrentPassword))
        {
            return IdentityErrors.WrongCurrentPassword;
        }

        if (userReturnResult.Value.Email is null)
        {
            return UserErrors.NotFound;
        }

        await unitOfWork.BeginTransactionAsync();
        var result = await authService.ChangePasswordAsync(userId, dto.CurrentPassword, dto.NewPassword);

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

    public async Task<ErrorOr<Success>> ForgotPassword(ForgetPasswordRequestDto dto)
    {
        var userReturnResult = await userService.GetByEmailAsync(dto.Email);
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

    public async Task<ErrorOr<Success>> ResetForgetPassword(ResetForgetPasswordRequestDto dto)
    {
        var validationResult = await _forgotPasswordValidator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            return validationResult.ExtractErrors();
        }

        var userReturnResult = await userService.GetByIdAsync(dto.UserId);
        if (userReturnResult.IsError)
        {
            return userReturnResult.Errors;
        }

        var user = userReturnResult.Value;
        if (user.Email is null)
        {
            return UserErrors.NotFound;
        }


        if (!await authService.ValidateResetPasswordToken(user.Id, dto.Token))
        {
            return IdentityErrors.InvalidResetPasswordToken;
        }

        await unitOfWork.BeginTransactionAsync();

        if (!await authService.ResetPasswordAsync(user.Id, dto.Token, dto.NewPassword))
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