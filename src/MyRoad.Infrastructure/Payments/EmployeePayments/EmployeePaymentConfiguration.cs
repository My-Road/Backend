using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyRoad.Domain.Payments.EmployeePayments;

namespace MyRoad.Infrastructure.Payments.EmployeePayments;

public class EmployeePaymentConfiguration : IEntityTypeConfiguration<EmployeePayment>
{
    public void Configure(EntityTypeBuilder<EmployeePayment> builder)
    {
        builder.ToTable("EmployeePayment");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.Property(x => x.Amount)
            .HasColumnType("decimal(10,2)")
            .IsRequired();

        builder.Property(x => x.PaymentDate)
            .IsRequired();
        builder.Property(x => x.Notes)
            .HasColumnType("nvarchar(500)");

        builder.HasOne(x => x.Employee)
            .WithMany(e => e.Payments)
            .HasForeignKey(x => x.EmployeeId);
    }
}