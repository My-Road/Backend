using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using MyRoad.Domain.Identity.Enums;
using MyRoad.Domain.Identity.Interfaces;

namespace MyRoad.Domain.Identity;

public class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
{
    public long Id =>
        long.TryParse(httpContextAccessor.HttpContext?.User.FindFirst("uid")?.Value, out var userId)
            ? userId
            : throw new Exception("User ID claim is missing or invalid.");

    public UserRole Role =>
        Enum.TryParse(httpContextAccessor.HttpContext?.User.FindFirst("userRole")?.Value, out UserRole role)
            ? role
            : throw new Exception("User role claim is missing or invalid.");

    public string Email =>
        httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value
        ?? throw new Exception("Email claim is missing.");

    public long? TokenVersion =>
        long.TryParse(httpContextAccessor.HttpContext?.User.FindFirst("token_version")?.Value, out var tokenVersion)
            ? tokenVersion
            : throw new Exception("User tokenVersion claim is missing or invalid.");
}