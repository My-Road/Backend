using Microsoft.Extensions.DependencyInjection;
using MyRoad.Domain.Employees;
using MyRoad.Domain.Customers;
using MyRoad.Domain.Identity;
using MyRoad.Domain.Identity.Interfaces;
using MyRoad.Domain.Orders;
using MyRoad.Domain.Payments.CustomerPayments;
using MyRoad.Domain.Payments.EmployeePayments;
using MyRoad.Domain.Users;

namespace MyRoad.Domain;

public static class DomainConfiguration
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserContext, UserContext>();
        services.AddScoped<IEmployeePaymentService, EmployeePaymentService>();
        services.AddScoped<IEmployeeService,EmployeeService>();
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<ICustomerPaymentService, CustomerPaymentService>();

        return services;
    }
}