using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MyRoad.Infrastructure.Persistence;

public static class PersistenceConfiguration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration, IHostEnvironment environment)
    {
        services.AddDbContext<AppDbContext>(
            o =>
            {
                o.UseSqlServer(configuration.GetConnectionString("constr"));
                if (environment.IsDevelopment())
                {
                    o.EnableSensitiveDataLogging();
                    o.EnableDetailedErrors();
                }
            });


        return services;
    }
}