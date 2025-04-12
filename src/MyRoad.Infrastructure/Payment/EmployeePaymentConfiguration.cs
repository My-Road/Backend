using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyRoad.Domain.Payments;

namespace MyRoad.Infrastructure.Employee;

public class EmployeePaymentConfiguration : IEntityTypeConfiguration<EmployeePayment>
{
    public void Configure(EntityTypeBuilder<EmployeePayment> builder)
    {
        builder.ToTable("EmployeePayment");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.Amount)
            .HasColumnType("decimal(6,2)")
            .IsRequired();
            
        builder.Property(x => x.PaymentDate)
            .HasColumnType("datetime")
            .IsRequired();
        builder.Property(x => x.Notes).HasColumnType("nvarchar(500)");

        // builder.HasOne(x => x.Payment)
        //     .WithMany()
        //     .HasForeignKey(x => x.EmployeeId);
    }
}