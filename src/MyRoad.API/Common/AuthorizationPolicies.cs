using MyRoad.Domain.Identity.Enums;

namespace MyRoad.API.Common;

public static class AuthorizationPolicies
{
    public const string FactoryOwner = nameof(UserRole.FactoryOwner);
    public const string Admin = nameof(UserRole.Admin);
    public const string Manager = nameof(UserRole.Manager);
    public const string FactoryOwnerOrAdmin = $"{nameof(UserRole.FactoryOwner)},{nameof(UserRole.Admin)}";

    public const string FactoryOwnerOrAdminOrManager =
        $"{nameof(UserRole.FactoryOwner)},{nameof(UserRole.Admin)},{nameof(UserRole.Manager)}";
}