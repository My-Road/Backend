using MyRoad.Domain.Identity.Enums;

namespace MyRoad.Domain.Identity.Interfaces;

public interface IUserContext
{
    public long Id { get; }
    public UserRole Role { get; }
    public string Email { get; }
}