using MyRoad.Domain.Identity.Interfaces;
using MyRoad.Domain.Identity.RequestsDto;
using MyRoad.Domain.Identity.Responses;
using MyRoad.Domain.Identity.Validators;

namespace MyRoad.Domain.Identity.Services;

public class IdentityService(
    IAuthService authService,
    IJwtTokenGenerator tokenGenerator,
    IPasswordGenerationService passwordGenerationService)
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
            Role = [user.Role],
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
                IsCreate = false
            };
        }
        var password = passwordGenerationService.GenerateRandomPassword(8);
        var result = await authService.RegisterUser(dto, password);
        if (result is "")
        {
            return new RegisterResponsesDto
            {
                Message = "User registered successfully.",
                IsCreate = true,
                password = password // just for testing I will remove it when I finish test 
              
            };
        }


        return new RegisterResponsesDto
        {
            Message = result,
            IsCreate = false,
        };
    }
}