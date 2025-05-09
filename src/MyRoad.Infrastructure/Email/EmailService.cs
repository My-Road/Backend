using ErrorOr;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using MyRoad.Domain.Email;
using MailKit.Net.Smtp;

namespace MyRoad.Infrastructure.Email;

public class EmailService(IOptions<EmailSettings> emailSettings) : IEmailService
{
    private readonly EmailSettings _emailSettings = emailSettings.Value;

    public async Task<ErrorOr<Success>> SendEmailAsync(EmailRequest emailRequest)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(_emailSettings.FromEmail));

        foreach (var toEmail in emailRequest.ToEmails)
            email.To.Add(MailboxAddress.Parse(toEmail));

        email.Subject = emailRequest.Subject;
        email.Body = new TextPart(TextFormat.Html) { Text = emailRequest.Body };

        using var smtp = new SmtpClient();

        var errors = new List<Error>();

        if (emailRequest.ToEmails.Count == 0)
            errors.Add(EmailErrors.NoRecipient());

        try
        {
            await smtp.ConnectAsync(_emailSettings.Server, _emailSettings.Port, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_emailSettings.Username, _emailSettings.Password);
            await smtp.SendAsync(email);
        }
        finally
        {
            await smtp.DisconnectAsync(true);
        }

        return errors.Count > 0 ? errors : Result.Success;
    }
}