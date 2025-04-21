using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyRoad.Domain.Employees;
using MyRoad.Domain.Payments.EmployeePayments;
using MyRoad.Infrastructure.Employees;
using MyRoad.Infrastructure.Identity.Entities;
using MyRoad.Infrastructure.Payments;
using MyRoad.Infrastructure.Payments.EmployeePayments;
using MyRoad.Infrastructure.Persistence.config;

namespace MyRoad.Infrastructure.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options)
    : IdentityDbContext<ApplicationUser, IdentityRole<long>, long>(options)
{
    public DbSet<Employee> Employee { get; set; }
    public DbSet<EmployeePayment> EmployeePayment { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<ApplicationUser>()
            .Property(u => u.Role)
            .HasConversion<string>();
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.ApplyConfiguration(new EmployeePaymentConfiguration());
        modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
    }
}