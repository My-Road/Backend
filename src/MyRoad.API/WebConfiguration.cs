namespace MyRoad.API;

public static class WebConfiguration
{
    public static IServiceCollection AddWeb(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer()
            .AddSwaggerGen();

        services.AddControllers();

        return services;
    }
}