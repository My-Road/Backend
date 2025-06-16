using Asp.Versioning;
using AspNetCoreRateLimit;
using Microsoft.OpenApi.Models;
using MyRoad.Domain.Identity.Enums;
using MyRoad.API.Common;

namespace MyRoad.API;

public static class WebConfiguration
{
    public static IServiceCollection AddWeb(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddEndpointsApiExplorer()
            .AddSwaggerGen();
        services.AddApiVersioning(opts =>
        {
            opts.AssumeDefaultVersionWhenUnspecified = true;
            opts.DefaultApiVersion = new ApiVersion(1, 0);
            opts.ReportApiVersions = true;
            opts.ApiVersionReader = new UrlSegmentApiVersionReader();
            opts.UnsupportedApiVersionStatusCode = StatusCodes.Status406NotAcceptable;
        }).AddApiExplorer(options => options.GroupNameFormat = "'v'VVV");

        services.AddControllers();
        services.AddCors(c =>
        {
            c.AddDefaultPolicy(options =>
                options.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
        });

        services.AddApiVersioning(opts =>
        {
            opts.AssumeDefaultVersionWhenUnspecified = true;
            opts.DefaultApiVersion = new ApiVersion(1, 0);
            opts.ReportApiVersions = true;
            opts.ApiVersionReader = new UrlSegmentApiVersionReader();
            opts.UnsupportedApiVersionStatusCode = StatusCodes.Status406NotAcceptable;
        }).AddApiExplorer(options => options.GroupNameFormat = "'v'VVV");
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
        services.AddAuthorizationPolicy();
        
        
        services.AddMemoryCache();

       

        services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
        services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
        services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
        services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
        
        return services;
    }

    private static void AddAuthorizationPolicy(this IServiceCollection services)
    {
        services.AddAuthorizationBuilder()
            .AddPolicy(AuthorizationPolicies.FactoryOwner,
                policy => policy.RequireClaim("userRole", UserRole.FactoryOwner.ToString()))
            
            .AddPolicy(AuthorizationPolicies.Admin,
                policy => policy.RequireClaim("userRole", UserRole.Admin.ToString()))
            
            .AddPolicy(AuthorizationPolicies.Manager,
                policy => policy.RequireClaim("userRole", UserRole.Manager.ToString()))
            
            .AddPolicy(AuthorizationPolicies.FactoryOwnerOrAdmin,
                policy => policy.RequireClaim("userRole", UserRole.FactoryOwner.ToString(),
                    UserRole.Admin.ToString()))
            
            .AddPolicy(AuthorizationPolicies.FactoryOwnerOrAdminOrManager,
                policy => policy.RequireClaim("userRole", UserRole.FactoryOwner.ToString(),
                    UserRole.Admin.ToString(),
                    UserRole.Manager.ToString()));
    }
}