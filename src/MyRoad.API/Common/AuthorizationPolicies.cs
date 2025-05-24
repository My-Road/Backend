using MyRoad.Domain.Identity.Enums;

namespace MyRoad.API.Common;

public static class AuthorizationPolicies
{
    public const string SuperAdmin = nameof(UserRole.SuperAdmin);
    public const string Admin = nameof(UserRole.Admin);
    public const string Manager = nameof(UserRole.Manager);
    public const string AdminOrManager = $"{nameof(UserRole.Admin)},{nameof(UserRole.Manager)}";
}