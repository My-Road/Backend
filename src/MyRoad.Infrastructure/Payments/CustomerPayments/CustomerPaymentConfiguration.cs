using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyRoad.Domain.Payments.CustomerPayments;

namespace MyRoad.Infrastructure.Payments.CustomerPayments;

public class CustomerPaymentConfiguration : IEntityTypeConfiguration<CustomerPayment>
{
    public void Configure(EntityTypeBuilder<CustomerPayment> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.Property(x => x.Amount)
            .HasColumnType("decimal(10,2)")
            .IsRequired();

        builder.Property(x => x.PaymentDate)
            .HasColumnType("datetime")
            .IsRequired();

        builder.Property(x => x.Notes)
            .HasMaxLength(500);

        builder.HasOne(x => x.Customer)
            .WithMany(e => e.CustomerPayments)
            .HasForeignKey(x => x.CustomerId)
            .IsRequired();

        builder.ToTable("CustomerPayment");
    }
}