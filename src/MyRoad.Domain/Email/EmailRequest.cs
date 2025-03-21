namespace MyRoad.Domain.Email;

public class EmailRequest
{
    public List<string> ToEmails { get; set; } = [string.Empty];
    public string Subject { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
}