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

    public async Task SendEmailAsync(EmailRequest emailRequest)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(_emailSettings.FromEmail));

        foreach (var toEmail in emailRequest.ToEmails)
            email.To.Add(MailboxAddress.Parse(toEmail));

        email.Subject = emailRequest.Subject;
        email.Body = new TextPart(TextFormat.Html)
        {
            Text = emailRequest.Body
        };

        using var smtp = new SmtpClient();
        try
        {
            await smtp.ConnectAsync(_emailSettings.Server, _emailSettings.Port, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_emailSettings.Username, _emailSettings.Password);
            await smtp.SendAsync(email);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Email sending failed: {ex.Message}");
        }
        finally
        {
            await smtp.DisconnectAsync(true);
        }
    }
}