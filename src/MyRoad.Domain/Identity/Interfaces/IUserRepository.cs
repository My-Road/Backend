using ErrorOr;
using MyRoad.Domain.Common.Entities;
using MyRoad.Domain.Users;
using Sieve.Models;

namespace MyRoad.Domain.Identity.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(long id);
    
    Task<User?> GetByEmailAsync(string email);
    
    Task<ErrorOr<PaginatedResponse<User>>> GetAsync(SieveModel sieveModel);
}