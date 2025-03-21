using Microsoft.Extensions.Logging;
using MyRoad.Domain.Email;
using MyRoad.Domain.Identity.Enums;
using MyRoad.Domain.Identity.Interfaces;
using MyRoad.Domain.Identity.RequestsDto;
using MyRoad.Domain.Identity.Responses;
using MyRoad.Domain.Identity.Validators;

namespace MyRoad.Domain.Identity.Services;

public class IdentityService(
    IAuthService authService,
    IJwtTokenGenerator tokenGenerator,
    IPasswordGenerationService passwordGenerationService,
    IEmailService emailService)
    : IIdentityService
{
    private readonly RegisterValidator _registerValidator = new();

    public async Task<LoginResponseDto> Login(LoginRequestDto dto)
    {
        var user = await authService.AuthenticateAsync(dto);

        if (user is null)
        {
            return new LoginResponseDto
            {
                IsAuthenticated = false,
                Message = "Invalid email or password."
            };
        }

        var accessToken = tokenGenerator.CreateJwtToken(user);

        return new LoginResponseDto
        {
            IsAuthenticated = true,
            Role = Enum.GetName(user.Role) ?? "UnknownRole",
            Token = accessToken.Token,
            ExpiresOn = accessToken.ExpiresOn
        };
    }

    public async Task<RegisterResponsesDto> Register(RegisterRequestDto dto)
    {
        var validationResult = await _registerValidator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            return new RegisterResponsesDto()
            {
                Message = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)),
                IsCreated = false
            };
        }

        var password = passwordGenerationService.GenerateRandomPassword(8);
        var result = await authService.RegisterUser(dto, password);
        if (result is not "")
            return new RegisterResponsesDto
            {
                Message = result,
                IsCreated = false,
            };

        await emailService.SendEmailAsync(new EmailRequest()
        {
            ToEmails = [dto.Email],
            Subject = "welcome in MyRoad Company",
            Body = EmailUtils.GetRegistrationEmailBody(dto.Email, dto.FirstName + " " + dto.LastName, password,
                "loginlink")
        });
        return new RegisterResponsesDto
        {
            Message = "User registered successfully.",
            IsCreated = true,
        };
    }
}