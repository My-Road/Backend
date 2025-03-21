using System.Text;
using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MyRoad.Domain.Identity.Enums;
using MyRoad.Infrastructure.Persistence.config;

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
        return services;
    }

    private static void AddAuthorizationPolicy(this IServiceCollection services)
    {
        services.AddAuthorizationBuilder()
            .AddPolicy("Admin", policy => policy.RequireRole(UserRole.Admin.ToString()))
            .AddPolicy("SuperAdmin", policy => policy.RequireRole(UserRole.SuperAdmin.ToString()))
            .AddPolicy("Manager", policy => policy.RequireRole(UserRole.Manager.ToString()));
    }
}