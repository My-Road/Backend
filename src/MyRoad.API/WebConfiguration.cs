namespace MyRoad.API;

public static class WebConfiguration
{
    public static IServiceCollection AddWeb(this IServiceCollection services)
    {
        return services.AddEndpointsApiExplorer()
            .AddSwaggerGen();
    }
}