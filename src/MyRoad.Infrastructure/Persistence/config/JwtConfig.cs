namespace MyRoad.Infrastructure.Persistence.config;

public class JwtConfig
{
    public string Key { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public double DurationInDays { get; set; }
    
    public double ResetPasswordTokenLifeTimeMinutes { get; set; }
}