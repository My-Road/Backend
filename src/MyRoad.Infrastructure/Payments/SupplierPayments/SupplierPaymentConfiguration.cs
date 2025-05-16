using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyRoad.Domain.Payments.SupplierPayments;

namespace MyRoad.Infrastructure.Payments.SupplierPayments
{
    public class SupplierPaymentConfiguration : IEntityTypeConfiguration<SupplierPayment>
    {
        public void Configure(EntityTypeBuilder<SupplierPayment> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Amount)
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.Property(x => x.PaymentDate)
                .IsRequired();

            builder.Property(x => x.Notes)
                .HasMaxLength(500);

            builder.HasOne(x => x.Supplier)
                .WithMany(x => x.SupplierPayments)
                .HasForeignKey(x => x.SupplierId)
                .IsRequired();

            builder.ToTable("SupplierPayment");
        }
    }
}
