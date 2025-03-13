using MyRoad.Domain.Identity.RequestsDto;
using MyRoad.Domain.Identity.Responses;

namespace MyRoad.Domain.Identity.Services;

public interface IAuthService
{
    Task<LoginResponseDto> Login(LoginRequestDto dto);
}