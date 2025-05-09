using MyRoad.Domain.Users;
using ErrorOr;

namespace MyRoad.Domain.Identity.Interfaces;

public interface IAuthService
{
    Task<ErrorOr<User>> AuthenticateAsync(string email, string password);

    Task<ErrorOr<bool>> RegisterUser(User user, string password);

    Task<ErrorOr<bool>> ChangePasswordAsync(long userId, string currentPassword, string newPassword);

    Task<bool> IsOwnPasswordAsync(long userId, string password);

    Task<string> GenerateResetPasswordTokenAsync(long userId);

    Task<bool> ValidateResetPasswordToken(long userId, string token);

    Task<bool> ResetPasswordAsync(long userId, string token, string newPassword);
}