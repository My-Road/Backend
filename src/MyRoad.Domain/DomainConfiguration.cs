using Microsoft.Extensions.DependencyInjection;
using MyRoad.Domain.Products;

namespace MyRoad.Domain;

public static class DomainConfiguration
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        services.AddScoped<IProductService, ProductService>();
        
        return services;
    }
}