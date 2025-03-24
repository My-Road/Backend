using MyRoad.Domain.Identity.RequestsDto;
using MyRoad.Domain.Users;
using ErrorOr;
namespace MyRoad.Domain.Identity.Interfaces;

public interface IAuthService
{
    Task<ErrorOr<User>> AuthenticateAsync(LoginRequestDto dto);
    Task<ErrorOr<bool>> RegisterUser(RegisterRequestDto registerRequestDto, string password);
    Task<ErrorOr<bool>> ChangePasswordAsync(string userId, string currentPassword, string newPassword);
    public Task<bool> IsOwnPasswordAsync(string userId, string password);
}