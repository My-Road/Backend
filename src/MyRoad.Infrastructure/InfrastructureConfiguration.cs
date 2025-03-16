using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyRoad.Domain.Identity.Interfaces;
using MyRoad.Domain.Identity.Services;
using MyRoad.Infrastructure.Identity;
using MyRoad.Infrastructure.Identity.Entities;
using MyRoad.Infrastructure.Persistence;

namespace MyRoad.Infrastructure;

public static class InfrastructureConfiguration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration builderConfiguration, IHostEnvironment environment)
    {
        services.AddInfrastructureServices(builderConfiguration, environment);
        services.AddIdentity<ApplicationUser, IdentityRole<long>>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 6;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IIdentityService, IdentityService>();
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<IPasswordGenerationService, PasswordGenerationService>();
        return services;
    }
}