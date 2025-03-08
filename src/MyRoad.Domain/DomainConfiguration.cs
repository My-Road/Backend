using Microsoft.Extensions.DependencyInjection;
using MyRoad.Domain.Entities.Auth;


namespace MyRoad.Domain;

public static class DomainConfiguration
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        // services.AddScoped<IAuthService, AuthService>();
        return services;
    }
}