using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyRoad.Domain.Email;

namespace MyRoad.Infrastructure.Email;

public static class EmailConfiguration
{
    public static IServiceCollection ConfigureEmail(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<EmailSettings>()
            .BindConfiguration(nameof(EmailSettings));

        services.AddScoped<IEmailService, EmailService>();

        return services;
    }
}