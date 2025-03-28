using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using MyRoad.Domain.Identity.Enums;
using MyRoad.Domain.Identity.Interfaces;

namespace MyRoad.Domain.Identity;

public class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
{
    public long Id
    {
        get
        {
            var claim = httpContextAccessor.HttpContext?.User.FindFirst("uid");
            return claim != null && long.TryParse(claim.Value, out var userId) ? userId : 0;
        }
    }
    
    public UserRole Role
    {
        get
        {
            var roleClaim = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Role);
            return roleClaim != null && Enum.TryParse(roleClaim.Value, out UserRole role) ? role : UserRole.Unknown;
        }
    }

    public string Email
    {
        get
        {
            var emailClaim = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email);
            return emailClaim?.Value ?? "Unknown Email";  
        }
    }
}