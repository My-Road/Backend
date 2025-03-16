using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MyRoad.Domain.Identity.Interfaces;
using MyRoad.Domain.Identity.Models;
using MyRoad.Domain.Users;
using MyRoad.Infrastructure.Persistence.config;

namespace MyRoad.Infrastructure.Identity;

public class JwtTokenGenerator(IOptions<JWT> jwt) : IJwtTokenGenerator
{
    private readonly JWT _jwt = jwt.Value;

    public AccessToken CreateJwtToken(User user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim("uid", user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _jwt.Issuer,
            audience: _jwt.Audience,
            claims: claims,
            expires: DateTime.Now.AddDays(_jwt.DurationInDays),
            signingCredentials: signingCredentials);

        var token = new JwtSecurityTokenHandler()
            .WriteToken(jwtSecurityToken);

        return new AccessToken
        {
            Token = token,
            ExpiresOn = jwtSecurityToken.ValidTo
        };
    }
}