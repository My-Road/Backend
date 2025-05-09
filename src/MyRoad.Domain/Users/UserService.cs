using ErrorOr;
using MyRoad.Domain.Identity.Interfaces;

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
}