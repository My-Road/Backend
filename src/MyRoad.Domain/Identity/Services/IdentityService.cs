using MyRoad.Domain.Identity.Interfaces;
using MyRoad.Domain.Identity.RequestsDto;
using MyRoad.Domain.Identity.Responses;

namespace MyRoad.Domain.Identity.Services;

public class IdentityService(IAuthService authService, IJwtTokenGenerator tokenGenerator) : IIdentityService
{
    public async Task<LoginResponseDto> Login(LoginRequestDto dto)
    {
        var loginResponseDto = new LoginResponseDto();

        var user = await authService.AuthenticateAsync(dto);
        if (user is null)
        {
            loginResponseDto.IsAuthenticated = false;
            loginResponseDto.Message = "Email or password is incorrect.";
            return loginResponseDto;
        }

        var accessToken = tokenGenerator.CreateJwtToken(user);
        loginResponseDto.IsAuthenticated = true;
        loginResponseDto.Role = [user.Role];
        loginResponseDto.Token = accessToken.Token;
        loginResponseDto.ExpiresOn = accessToken.ExpiresOn;

        return loginResponseDto;
    }
}