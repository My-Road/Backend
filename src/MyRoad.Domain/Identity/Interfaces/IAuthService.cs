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
    public Task<string> GenerateResetPasswordTokenAsync(long userId);
    public Task<bool> ValidateResetPasswordToken(long userId, string token);
    Task<bool> ResetPasswordAsync(long userId, string token, string newPassword);
}