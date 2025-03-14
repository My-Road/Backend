using MyRoad.Domain.Identity.RequestsDto;
using MyRoad.Domain.Users;

namespace MyRoad.Domain.Identity.Interfaces;

public interface IAuthService
{
    Task<User?> AuthenticateAsync(LoginRequestDto dto);
}