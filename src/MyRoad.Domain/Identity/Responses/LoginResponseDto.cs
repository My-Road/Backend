namespace MyRoad.Domain.Identity.Responses;

public class LoginResponseDto
{
    public string Token { get; set; }
    public string Message { get; set; }
    public bool IsAuthenticated { get; set; }
    public List<string> Role { get; set; }
    public DateTime ExpiresOn { get; set; }
}