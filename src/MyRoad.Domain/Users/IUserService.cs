namespace MyRoad.Domain.Users;

using ErrorOr;

public interface IUserService
{
    public Task<ErrorOr<User>> GetByIdAsync(string id);
    public Task<ErrorOr<User>> GetByEmailAsync(string email);
}