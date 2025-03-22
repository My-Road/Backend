using ErrorOr;
using MyRoad.Domain.Identity.RequestsDto;
using MyRoad.Domain.Identity.Responses;

namespace MyRoad.Domain.Identity.Interfaces;

public interface IIdentityService
{
    Task<ErrorOr<LoginResponseDto>> Login(LoginRequestDto dto);
    Task<ErrorOr<RegisterResponsesDto>> Register(RegisterRequestDto dto);
}