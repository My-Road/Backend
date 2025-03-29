using Microsoft.Extensions.DependencyInjection;
using MyRoad.Domain.Identity;
using MyRoad.Domain.Identity.Interfaces;
using MyRoad.Domain.Users;

namespace MyRoad.Domain;

public static class DomainConfiguration
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserContext, UserContext>();
        return services;
    }
}