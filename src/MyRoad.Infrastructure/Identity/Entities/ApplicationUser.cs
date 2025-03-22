using Microsoft.AspNetCore.Identity;
using MyRoad.Domain.Identity.Enums;

namespace MyRoad.Infrastructure.Identity.Entities;

public class ApplicationUser : IdentityUser<long>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public UserRole  Role { get; set; }
    public bool IsActive { get; set; }
    public string? PhoneNumber { get; set; }
}