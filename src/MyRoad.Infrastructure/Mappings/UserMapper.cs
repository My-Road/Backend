using MyRoad.Domain.Identity.Enums;
using MyRoad.Domain.Users;
using MyRoad.Infrastructure.Identity.Entities;
using Riok.Mapperly.Abstractions;

namespace MyRoad.Infrastructure.Mappings;


[Mapper]
public static partial class UserMapper
{

    public static User ToDomain(this ApplicationUser applicationUser)
    {
        var user = applicationUser.ToDomainInternal();

        user.Role = Enum.Parse<UserRole>(applicationUser.Role.ToString());

        return user;
    }
    private static partial User ToDomainInternal(this ApplicationUser userEntity);
}