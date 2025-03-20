namespace MyRoad.Domain.Email;

public interface IEmailService
{
    Task SendEmailAsync(EmailRequest emailRequest);
}