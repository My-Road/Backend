namespace MyRoad.Domain.Users;

using ErrorOr;

public interface IUserService
{
    public Task<ErrorOr<User>> GetByIdAsync(long id);
    public Task<ErrorOr<User>> GetByEmailAsync(string email);
}