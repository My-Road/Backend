using ErrorOr;
using MyRoad.Domain.Identity.Models;
using MyRoad.Domain.Users;


namespace MyRoad.Domain.Identity.Interfaces;

public interface IIdentityService
{
    Task<ErrorOr<AccessToken>> Login(string email, string password);
    Task<ErrorOr<Success>> Register(User user);
    Task<ErrorOr<Success>> ChangePassword(string currentPassword, string newPassword);
    Task<ErrorOr<Success>> ForgotPassword(string email);

    Task<ErrorOr<Success>> ResetForgetPassword(long userId, string token, string newPassword,
        string confirmNewPassword);
}