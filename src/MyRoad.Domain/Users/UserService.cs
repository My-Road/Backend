using ErrorOr;
using MyRoad.Domain.Common;
using MyRoad.Domain.Common.Entities;
using MyRoad.Domain.Identity.Enums;
using MyRoad.Domain.Identity.Interfaces;
using MyRoad.Domain.Identity.Validators;
using Sieve.Models;

namespace MyRoad.Domain.Users;

public class UserService(
    IUserRepository userRepository) : IUserService
{
    private readonly RegisterValidator _registerValidator = new();

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

    public async Task<ErrorOr<Success>> ToggleStatus(long id)
    {
        var user = await userRepository.GetByIdAsync(id);

        if (user is null)
        {
            return UserErrors.NotFoundId;
        }

        user.IsActive = !user.IsActive;
        await userRepository.UpdateAsync(user);
        return new Success();
    }

    public async Task<ErrorOr<Success>> ChangeRole(long id, UserRole role)
    {
        var user = await userRepository.GetByIdAsync(id);
        if (user is null)
        {
            return UserErrors.NotFoundId;
        }

        user.Role = role;
        var validator = await _registerValidator.ValidateAsync(user);
        if (!validator.IsValid)
        {
            return validator.ExtractErrors();
        }

        await userRepository.UpdateAsync(user);
        return new Success();
    }
}