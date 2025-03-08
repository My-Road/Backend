using Microsoft.AspNetCore.Identity;

namespace MyRoad.Domain.Entities.Auth;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}