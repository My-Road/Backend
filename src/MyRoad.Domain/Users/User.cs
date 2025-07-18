using MyRoad.Domain.Common.Entities;
using MyRoad.Domain.Identity.Enums;

namespace MyRoad.Domain.Users;

public class User : BaseEntity<long>
{
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public UserRole Role { get; set; }
    public bool IsActive { get; set; }
    public string? PhoneNumber { get; set; }
    public long TokenVersion { get; set; } = 1;
}