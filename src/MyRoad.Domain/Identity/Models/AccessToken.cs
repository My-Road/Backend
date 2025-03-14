namespace MyRoad.Domain.Identity.Models;

public class AccessToken
{
    public string Token { get; set; }
    public DateTime ExpiresOn { get; set; }
}