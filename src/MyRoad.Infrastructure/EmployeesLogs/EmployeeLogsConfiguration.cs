using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyRoad.Domain.EmployeesLogs;
using MyRoad.Infrastructure.Identity.Entities;

namespace MyRoad.Infrastructure.EmployeesLogs;

public class EmployeeLogsConfiguration : IEntityTypeConfiguration<EmployeeLog>
{
    public void Configure(EntityTypeBuilder<EmployeeLog> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x=>x.Date).IsRequired();
        builder.Property(x => x.CheckIn).IsRequired();
        builder.Property(x => x.CheckOut).IsRequired();
        builder.Property(x => x.Notes).HasMaxLength(500);
        builder.Property(x => x.CreatedByUserId).IsRequired();
        builder.Property(x => x.HourlyWage).IsRequired()
            .HasColumnType("decimal(4,2)");
        builder.Ignore(x => x.DailyWage);
        builder.Ignore(x => x.TotalHours);

        builder.HasOne(x => x.Employee)
            .WithMany(e => e.Logs)
            .HasForeignKey(x => x.EmployeeId);


        builder.HasOne<ApplicationUser>()
            .WithMany()
            .HasForeignKey(x => x.CreatedByUserId)
            .IsRequired();

        builder.ToTable("EmployeeLog");
    }
}