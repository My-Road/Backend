using ErrorOr;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyRoad.Domain.Common.Entities;
using MyRoad.Domain.Identity.Interfaces;
using MyRoad.Domain.Users;
using MyRoad.Infrastructure.Identity.Entities;
using MyRoad.Infrastructure.Mappings;
using MyRoad.Infrastructure.Persistence;
using Sieve.Models;
using Sieve.Services;

namespace MyRoad.Infrastructure.Users;

public class UserRepository(
    UserManager<ApplicationUser> userManager,
    AppDbContext dbContext,
    ISieveProcessor sieveProcessor,
    IUserContext userContext) : IUserRepository
{
    public async Task<User?> GetByIdAsync(long id)
    {
        var applicationUser = await userManager.FindByIdAsync(id.ToString());

        return applicationUser?.ToDomain();
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        var applicationUser = await userManager.FindByEmailAsync(email);
        return applicationUser?.ToDomain();
    }

    public async Task<ErrorOr<PaginatedResponse<User>>> GetAsync(SieveModel sieveModel)
    {
        var query = dbContext.Users
            .Where(user => user.Id != userContext.Id)
            .AsQueryable();

        var totalItems = await sieveProcessor
            .Apply(sieveModel, query, applyPagination: false)
            .CountAsync();

        query = sieveProcessor.Apply(sieveModel, query);

        var result = await query.AsNoTracking().ToListAsync();

        return new PaginatedResponse<User>
        {
            Items = result.Select(user => user.ToDomain()).ToList(),
            TotalCount = totalItems,
            Page = sieveModel.Page ?? 1,
            PageSize = sieveModel.PageSize ?? 10,
        };
    }

    public async Task UpdateAsync(User user)
    {
        var userToUpdate = await userManager.FindByIdAsync(user.Id.ToString());
        if (userToUpdate is null)
        {
            return;
        }

        userToUpdate.MapUpdatedApplicationUser(user);
        await userManager.UpdateAsync(userToUpdate);
        await dbContext.SaveChangesAsync();
    }
}