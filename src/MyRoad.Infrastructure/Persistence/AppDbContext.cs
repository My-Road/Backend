using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyRoad.Domain.Customers;
using MyRoad.Domain.Employees;
using MyRoad.Domain.EmployeesLogs;
using MyRoad.Domain.Orders;
using MyRoad.Domain.Payments.CustomerPayments;
using MyRoad.Domain.Payments.EmployeePayments;
using MyRoad.Domain.Payments.SupplierPayments;
using MyRoad.Domain.Purchases;
using MyRoad.Domain.Suppliers;
using MyRoad.Infrastructure.Customers;
using MyRoad.Infrastructure.Employees;
using MyRoad.Infrastructure.EmployeesLogs;
using MyRoad.Infrastructure.Identity.Entities;
using MyRoad.Infrastructure.Orders;
using MyRoad.Infrastructure.Payments.CustomerPayments;
using MyRoad.Infrastructure.Payments.EmployeePayments;
using MyRoad.Infrastructure.Payments.SupplierPayments;
using MyRoad.Infrastructure.Persistence.config;
using MyRoad.Infrastructure.Purchases;
using MyRoad.Infrastructure.Suppliers;

namespace MyRoad.Infrastructure.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options)
    : IdentityDbContext<ApplicationUser, IdentityRole<long>, long>(options)
{
    public DbSet<Employee> Employees { get; set; }
    public DbSet<EmployeePayment> EmployeePayments { get; set; }
    public DbSet<EmployeeLog> EmployeeLogs { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<CustomerPayment> CustomerPayments { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<Purchase> Purchases { get; set; }
    public DbSet<SupplierPayment> SupplierPayments { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<ApplicationUser>()
            .Property(u => u.Role)
            .HasConversion<string>();
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new EmployeePaymentConfiguration());
        modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
        modelBuilder.ApplyConfiguration(new EmployeeLogsConfiguration());
        modelBuilder.ApplyConfiguration(new OrderConfiguration());
        modelBuilder.ApplyConfiguration(new CustomerPaymentConfiguration());
        modelBuilder.ApplyConfiguration(new CustomerConfiguration());
        modelBuilder.ApplyConfiguration(new SupplierConfiguration());
        modelBuilder.ApplyConfiguration(new PurchaseConfiguration());
        modelBuilder.ApplyConfiguration(new SupplierPaymentConfiguration());
    }
}