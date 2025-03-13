using System.IdentityModel.Tokens.Jwt;
using MyRoad.Domain.Users;

namespace MyRoad.Domain.Identity.Services;

public interface IJwtTokenGenerator
{
    Task<JwtSecurityToken> CreateJwtToken(User user);
    
}