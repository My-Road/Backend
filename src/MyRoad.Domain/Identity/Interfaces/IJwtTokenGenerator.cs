using System.IdentityModel.Tokens.Jwt;
using MyRoad.Domain.Identity.Models;
using MyRoad.Domain.Users;

namespace MyRoad.Domain.Identity.Interfaces;

public interface IJwtTokenGenerator
{
    AccessToken CreateJwtToken(User user);
}