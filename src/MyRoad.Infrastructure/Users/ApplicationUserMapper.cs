using MyRoad.Domain.Identity.Enums;
using MyRoad.Domain.Users;
using MyRoad.Infrastructure.Identity.Entities;

namespace MyRoad.Infrastructure.Users;

public static class ApplicationUserMapper
{
    public static void MapUpdatedApplicationUser(this ApplicationUser applicationUser, User user)
    {
        applicationUser.Email = user.Email;
        applicationUser.NormalizedEmail = user.Email?.ToUpper();
        applicationUser.Role = Enum.Parse<UserRole>(user.Role.ToString());
        applicationUser.FirstName = user.FirstName;
        applicationUser.LastName = user.LastName;
        applicationUser.IsActive = user.IsActive;
        applicationUser.PhoneNumber = user.PhoneNumber;
        applicationUser.UserName = user.Email;
        applicationUser.TokenVersion = user.TokenVersion;
    }
}