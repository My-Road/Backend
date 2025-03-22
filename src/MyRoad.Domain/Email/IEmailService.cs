namespace MyRoad.Domain.Email;
using ErrorOr;
public interface IEmailService
{
    Task<ErrorOr<Success>> SendEmailAsync(EmailRequest emailRequest);
}