using Microsoft.IdentityModel.Tokens;
using MyRoad.Domain.Identity.RequestsDto;
using MyRoad.Domain.Users;
using ValidationFailure = FluentValidation.Results.ValidationFailure;

namespace MyRoad.Domain.Identity.Interfaces;

public interface IAuthService
{
    Task<User?> AuthenticateAsync(LoginRequestDto dto);
    Task<string?> RegisterUser(RegisterRequestDto registerRequestDto, string password);
}