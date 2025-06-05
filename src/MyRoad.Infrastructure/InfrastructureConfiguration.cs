using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using MyRoad.Domain.Customers;
using MyRoad.Domain.Employees;
using MyRoad.Domain.EmployeesLogs;
using MyRoad.Domain.Identity.Interfaces;
using MyRoad.Domain.Identity.Services;
using MyRoad.Domain.Orders;
using MyRoad.Domain.Payments.CustomerPayments;
using MyRoad.Domain.Payments.EmployeePayments;
using MyRoad.Domain.Payments.SupplierPayments;
using MyRoad.Domain.Purchases;
using MyRoad.Domain.Suppliers;
using MyRoad.Domain.Users;
using MyRoad.Infrastructure.Customers;
using MyRoad.Infrastructure.Email;
using MyRoad.Infrastructure.Employees;
using MyRoad.Infrastructure.EmployeesLogs;
using MyRoad.Infrastructure.Identity;
using MyRoad.Infrastructure.Identity.Entities;
using MyRoad.Infrastructure.Orders;
using MyRoad.Infrastructure.Payments.CustomerPayments;
using MyRoad.Infrastructure.Payments.EmployeePayments;
using MyRoad.Infrastructure.Payments.SupplierPayments;
using MyRoad.Infrastructure.Persistence;
using MyRoad.Infrastructure.Persistence.config;
using MyRoad.Infrastructure.Purchases;
using MyRoad.Infrastructure.Suppliers;
using MyRoad.Infrastructure.Users;
using Sieve.Models;
using Sieve.Services;

namespace MyRoad.Infrastructure;

public static class InfrastructureConfiguration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration builderConfiguration, IHostEnvironment environment)
    {
        services.AddInfrastructureServices(builderConfiguration, environment);
        services.ConfigureEmail(builderConfiguration);
        UserApplicationOptions(services);

        services.AddJwtAuthentication(builderConfiguration);

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IIdentityService, IdentityService>();
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<IPasswordGenerationService, PasswordGenerationService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IEmployeePaymentRepository, EmployeePaymentRepository>();
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<ISieveProcessor, SieveProcessor>();
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IEmployeeLogRepository, EmployeeLogRepository>();
        services.AddScoped<IPurchaseRepository, PurchaseRepository>();
        services.AddScoped<ISupplierRepository, SupplierRepository>();
        services.AddScoped<ICustomerPaymentRepository, CustomerPaymentRepository>();
        services.AddScoped<ISupplierPaymentRepository, SupplierPaymentRepository>();
        services.AddScoped<ISieveConfiguration, CustomerSieveConfiguration>();
        SieveOption(services);
        return services;
    }
    
    private static void SieveOption(IServiceCollection services)
    {
        services.AddScoped<ISieveProcessor, MyRoadSieveProcessor>();
        services.Configure<SieveOptions>(options =>
        {
            options.CaseSensitive = false;
            options.ThrowExceptions = true;
            options.IgnoreNullsOnNotEqual = true;
        });
    }

    private static void UserApplicationOptions(IServiceCollection services)
    {
        services.AddIdentity<ApplicationUser, IdentityRole<long>>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 8;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();
        
        services.Configure<IdentityOptions>(options =>
        {
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;
        });
    }

    private static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<JwtConfig>()
            .BindConfiguration(nameof(JwtConfig));

        var jwtConfig = new JwtConfig();
        configuration.GetSection(nameof(JwtConfig)).Bind(jwtConfig);


        services.Configure<DataProtectionTokenProviderOptions>(opt =>
            opt.TokenLifespan = TimeSpan.FromMinutes(jwtConfig.ResetPasswordTokenLifeTimeMinutes));
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                var key = Encoding.UTF8.GetBytes(jwtConfig.Key);
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtConfig.Issuer,
                    ValidAudience = jwtConfig.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.Zero
                };
            });
    }
}