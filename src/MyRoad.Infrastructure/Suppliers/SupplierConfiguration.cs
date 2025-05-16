using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyRoad.Domain.Suppliers;

namespace MyRoad.Infrastructure.Suppliers
{
    public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.SupplierName).
                HasMaxLength(50).
                HasColumnType("nvarchar").
                IsRequired();

            builder.Property(x => x.Email)
                .HasMaxLength(200);

            builder.Property(x => x.PhoneNumber).
                HasColumnType("nvarchar").
                HasMaxLength(15).
                IsRequired();

            builder.Property(x => x.Address).
                HasColumnType("nvarchar").
                HasMaxLength(50);

            builder.Property(x => x.TotalDueAmount)
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.Property(x => x.TotalPaidAmount)
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.Ignore(x => x.RemainingAmount);

            builder.HasIndex(x => x.PhoneNumber).IsUnique();

            builder.ToTable("Supplier");
        }
    }
}
