using MyRoad.Domain.Identity.Enums;

namespace MyRoad.API.Users.ResponseDto;

public class UserDto
{
    public string? Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public UserRole Role { get; set; }
    public bool IsActive { get; set; }
    public string? PhoneNumber { get; set; }
}