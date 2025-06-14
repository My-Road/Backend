using Microsoft.Extensions.DependencyInjection;
using MyRoad.Domain.Customers;
using MyRoad.Domain.Employees;
using MyRoad.Domain.Dashboard;
using MyRoad.Domain.EmployeesLogs;
using MyRoad.Domain.Identity;
using MyRoad.Domain.Identity.Interfaces;
using MyRoad.Domain.Orders;
using MyRoad.Domain.Payments.CustomerPayments;
using MyRoad.Domain.Payments.EmployeePayments;
using MyRoad.Domain.Payments.SupplierPayments;
using MyRoad.Domain.Purchases;
using MyRoad.Domain.Reports;
using MyRoad.Domain.Suppliers;
using MyRoad.Domain.Users;

namespace MyRoad.Domain;

public static class DomainConfiguration
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserContext, UserContext>();
        services.AddScoped<IEmployeePaymentService, EmployeePaymentService>();
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<ICustomerPaymentService, CustomerPaymentService>();
        services.AddScoped<IEmployeeLogService, EmployeeLogService>();
        services.AddScoped<ITimeOverlapValidator, TimeOverlapValidator>();
        services.AddScoped<ISupplierService, SupplierService>();
        services.AddScoped<IPurchaseService, PurchaseService>();
        services.AddScoped<ISupplierPaymentService, SupplierPaymentService>();
        services.AddScoped<IDashboardOverviewService, DashboardOverviewService>();


        return services;
    }
}