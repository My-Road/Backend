using ErrorOr;
using MyRoad.Domain.Identity.RequestsDto;
using MyRoad.Domain.Identity.ResponsesDto;

namespace MyRoad.Domain.Identity.Interfaces;

public interface IIdentityService
{
    Task<ErrorOr<LoginResponseDto>> Login(LoginRequestDto dto);
    Task<ErrorOr<RegisterResponsesDto>> Register(RegisterRequestDto dto);
    Task<ErrorOr<Success>> ChangePassword(string userId, ChangePasswordRequestDto dto);
    Task<ErrorOr<Success>> ForgotPassword(ForgetPasswordRequestDto dto);
    Task<ErrorOr<Success>> ResetForgetPassword(ResetForgetPasswordRequestDto dto);
}