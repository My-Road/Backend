using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MyRoad.Infrastructure.Persistence;

public static class PersistenceConfiguration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        
        services.AddDbContext<AppDbContext>(
            o =>
                o.UseSqlServer(configuration.GetConnectionString("constr"))
            );
        
        
        return services;
    }
}