using ErrorOr;
using MyRoad.Domain.Common.Entities;
using MyRoad.Domain.Identity.Interfaces;
using MyRoad.Domain.Suppliers;
using Sieve.Models;

namespace MyRoad.Domain.Users;

public class UserService(
    IUserRepository userRepository
) : IUserService

{
    public async Task<ErrorOr<User>> GetByIdAsync(long id)
    {
        var user = await userRepository.GetByIdAsync(id);

        if (user is null)
        {
            return UserErrors.NotFound;
        }
        
        return user;
    }

    public async Task<ErrorOr<User>> GetByEmailAsync(string email)
    {
        var user = await userRepository.GetByEmailAsync(email);

        if (user is null)
        {
            return UserErrors.NotFound;
        }
        return user;
    }
    
    public async Task<ErrorOr<PaginatedResponse<User>>> GetAsync(SieveModel sieveModel)
    {
        var result = await userRepository.GetAsync(sieveModel);
        return result;
    }
}