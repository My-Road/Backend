using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyRoad.Domain.Payments;
using MyRoad.Infrastructure.Employee;
using MyRoad.Infrastructure.Identity.Entities;
using MyRoad.Infrastructure.Persistence.config;

namespace MyRoad.Infrastructure.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options)
    : IdentityDbContext<ApplicationUser, IdentityRole<long>, long>(options)
{
    public DbSet<EmployeePayment> EmployeePayments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<ApplicationUser>()
            .Property(u => u.Role)
            .HasConversion<string>();
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.ApplyConfiguration(new EmployeePaymentConfiguration());
    }
}