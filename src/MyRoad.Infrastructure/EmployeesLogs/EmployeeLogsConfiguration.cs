using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyRoad.Domain.EmployeesLogs;

namespace MyRoad.Infrastructure.EmployeesLogs;

public class EmployeeLogsConfiguration : IEntityTypeConfiguration<EmployeeLogs>
{
    public void Configure(EntityTypeBuilder<EmployeeLogs> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.CheckIn).IsRequired(false);
        builder.Property(x => x.CheckOut).IsRequired(false);
        builder.Property(x => x.IsWork).IsRequired();
        builder.Property(x=>x.Notes).HasMaxLength(500).IsRequired(false);
        builder.Property(x=>x.CreatedByUserId).IsRequired();
        builder.HasOne(x=>x.Employee).WithMany(e=>e.Logs).HasForeignKey(x=>x.EmployeeId);
        
        builder.ToTable("EmployeeLogs");

    }
}