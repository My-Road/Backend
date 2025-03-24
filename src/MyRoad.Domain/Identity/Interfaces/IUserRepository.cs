using MyRoad.Domain.Users;

namespace MyRoad.Domain.Identity.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(string id);
}