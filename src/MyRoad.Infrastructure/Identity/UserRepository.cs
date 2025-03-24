using Microsoft.AspNetCore.Identity;
using MyRoad.Domain.Identity.Interfaces;
using MyRoad.Domain.Users;
using MyRoad.Infrastructure.Identity.Entities;

namespace MyRoad.Infrastructure.Identity;

public class UserRepository(UserManager<ApplicationUser> userManager) : IUserRepository
{
    public async Task<User?> GetByIdAsync(string id)
    {
        var applicationUser = await userManager.FindByIdAsync(id);

        if (applicationUser == null)
        {
            return null;
        }

        return new User()
        {
            Id = applicationUser.Id,
            Email = applicationUser.Email,
            PhoneNumber = applicationUser.PhoneNumber,
            FirstName = applicationUser.FirstName,
            LastName = applicationUser.LastName,
            IsActive = applicationUser.IsActive,
        };
    }
}