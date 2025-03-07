using Microsoft.Extensions.DependencyInjection;
using MyRoad.Domain.Products;
using MyRoad.Infrastructure.Products;

namespace MyRoad.Infrastructure;

public static class InfrastructureConfiguration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IProductRepository, ProductRepository>();
        
        return services;
    }
}