using MyRoad.Domain.Identity.RequestsDto;
using MyRoad.Domain.Identity.Responses;

namespace MyRoad.Domain.Identity.Interfaces;

public interface IIdentityService
{
    Task<LoginResponseDto> Login(LoginRequestDto dto);
}