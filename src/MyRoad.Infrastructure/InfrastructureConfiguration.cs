using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyRoad.Infrastructure.Persistence;

namespace MyRoad.Infrastructure;

public static class InfrastructureConfiguration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration builderConfiguration, IHostEnvironment environment)
    {
        
        services.AddInfrastructureServices(builderConfiguration, environment);
        return services;
    }
}