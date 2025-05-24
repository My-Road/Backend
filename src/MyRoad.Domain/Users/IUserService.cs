using MyRoad.Domain.Common.Entities;
using Sieve.Models;

namespace MyRoad.Domain.Users;

using ErrorOr;

public interface IUserService
{
    Task<ErrorOr<User>> GetByIdAsync(long id);
    Task<ErrorOr<User>> GetByEmailAsync(string email);
    Task<ErrorOr<PaginatedResponse<User>>> GetAsync(SieveModel sieveModel);
}