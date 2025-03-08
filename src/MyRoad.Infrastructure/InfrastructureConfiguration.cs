using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyRoad.Infrastructure.Persistence;

namespace MyRoad.Infrastructure;

public static class InfrastructureConfiguration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration builderConfiguration)
    {
        // services.AddScoped<IProductRepository, ProductRepository>();
        services.AddInfrastructureServices(builderConfiguration);
        return services;
    }
}