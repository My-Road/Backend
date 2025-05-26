using MyRoad.Domain.Common.Entities;
using MyRoad.Domain.Identity.Enums;
using Sieve.Models;

namespace MyRoad.Domain.Users;

using ErrorOr;

public interface IUserService
{
    Task<ErrorOr<User>> GetByIdAsync(long id);

    Task<ErrorOr<User>> GetByEmailAsync(string email);

    Task<ErrorOr<Success>> UpdateAsync(long Id, User user);

    Task<ErrorOr<PaginatedResponse<User>>> GetAsync(SieveModel sieveModel);

    Task<ErrorOr<Success>> ToggleStatus(long id);

    Task<ErrorOr<Success>> ChangeRole(long id, UserRole role);
}