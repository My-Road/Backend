using ErrorOr;
using MyRoad.Domain.Common;
using MyRoad.Domain.Common.Entities;
using MyRoad.Domain.Identity.Enums;
using MyRoad.Domain.Identity.Interfaces;
using MyRoad.Domain.Identity.Validators;
using Sieve.Models;

namespace MyRoad.Domain.Users;

public class UserService(
    IUserRepository userRepository,
    IUserContext userContext) : IUserService
{
    private readonly RegisterValidator _registerValidator = new();
    private readonly UserValidator _userValidator = new();

    public async Task<ErrorOr<User>> GetByIdAsync(long id)
    {
        var user = await userRepository.GetByIdAsync(id);

        if (user is null || !user.IsActive)
        {
            return UserErrors.NotFound;
        }

        return user;
    }

    public async Task<ErrorOr<User>> GetByEmailAsync(string email)
    {
        var user = await userRepository.GetByEmailAsync(email);

        if (user is null || !user.IsActive)
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
        if (id == userContext.Id)
        {
            return UserErrors.CannotToggleOwnStatus;
        }

        var user = await userRepository.GetByIdAsync(id);

        if (user is null)
        {
            return UserErrors.NotFound;
        }

        user.IsActive = !user.IsActive;
        await userRepository.UpdateAsync(user);
        return new Success();
    }

    public async Task<ErrorOr<Success>> ChangeRole(long id, UserRole role)
    {
        if (id == userContext.Id)
        {
            return UserErrors.CannotChangeOwnRole;
        }

        var user = await userRepository.GetByIdAsync(id);
        if (user is null)
        {
            return UserErrors.NotFound;
        }

        user.Role = role;
        user.TokenVersion++;
        var validator = await _registerValidator.ValidateAsync(user);
        if (!validator.IsValid)
        {
            return validator.ExtractErrors();
        }

        await userRepository.UpdateAsync(user);
        return new Success();
    }

    public async Task<ErrorOr<Success>> UpdateAsync(long id, User user)
    {
        var existingUser = await userRepository.GetByIdAsync(id);

        if (existingUser is null || !existingUser.IsActive)
            return UserErrors.NotFound;

        var validationResult = await _userValidator.ValidateAsync(existingUser);
        if (!validationResult.IsValid)
            return validationResult.ExtractErrors();

        existingUser.MapUpdatedUser(user);
        await userRepository.UpdateAsync(existingUser);
        return new Success();
    }
}