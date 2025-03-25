using Microsoft.AspNetCore.Identity;
using MyRoad.Domain.Identity.Interfaces;
using MyRoad.Domain.Users;
using MyRoad.Infrastructure.Identity.Entities;
using MyRoad.Infrastructure.Mappings;

namespace MyRoad.Infrastructure.Identity;

public class UserRepository(UserManager<ApplicationUser> userManager) : IUserRepository
{
    public async Task<User?> GetByIdAsync(string id)
    {
        var applicationUser = await userManager.FindByIdAsync(id);

        return applicationUser?.ToDomain();
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        var applicationUser = await userManager.FindByEmailAsync(email);
        return applicationUser?.ToDomain();
    }
}