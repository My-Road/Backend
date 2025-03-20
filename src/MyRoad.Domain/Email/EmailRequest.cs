namespace MyRoad.Domain.Email;

public class EmailRequest
{
    public string ToEmails { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
}