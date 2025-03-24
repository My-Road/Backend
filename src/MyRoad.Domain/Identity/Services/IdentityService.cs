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
    private readonly ChangePasswordValidator _changePasswordValidator = new();

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
                "loginlink")
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

    public async Task<ErrorOr<ChangePasswordResponsesDto>> ChangePassword(string userId, ChangePasswordRequestDto dto)
    {
        var validationResult = await _changePasswordValidator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            return validationResult.ExtractErrors();
        }

        var user = await userService.GetByIdAsync(userId);
        if (user.IsError)
        {
            return UserErrors.NotFound;
        }

        if (!await authService.IsOwnPasswordAsync(userId, dto.CurrentPassword))
        {
            return IdentityErrors.WrongCurrentPassword;
        }

        await unitOfWork.BeginTransactionAsync();
        var result = await authService.ChangePasswordAsync(userId, dto.CurrentPassword, dto.NewPassword);

        if (result.IsError)
        {
            await unitOfWork.RollbackTransactionAsync();
            return result.Errors;
        }

        var user1 = user.Value;
        var emailResult = await emailService.SendEmailAsync(new EmailRequest()
        {
            ToEmails = [user1.Email],
            Subject = "Your password has been changed",
            Body = EmailUtils.GetPasswordResetSuccessEmailBody(user1.Email, user1.FirstName + " " + user1.LastName),
        });
        if (emailResult.IsError)
        {
            await unitOfWork.RollbackTransactionAsync();
            return emailResult.Errors;
        }

        await unitOfWork.CommitTransactionAsync();
        return new ChangePasswordResponsesDto
        {
            Message = "password has been changed",
            Success = true,
        };
    }
}