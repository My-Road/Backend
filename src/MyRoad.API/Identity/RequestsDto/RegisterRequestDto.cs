using MyRoad.Domain.Identity.Enums;

namespace MyRoad.API.Identity.RequestsDto;

public class RegisterRequestDto
{
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public UserRole Role { get; set; }
}