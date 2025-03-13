using Microsoft.AspNetCore.Identity;

namespace MyRoad.Infrastructure.Identity.Entities;

public class ApplicationUser : IdentityUser<long>
{
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Role { get; set; }
    public bool IsActive { get; set; }
    public string PhoneNumber { get; set; }
}