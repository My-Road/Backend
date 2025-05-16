using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyRoad.Domain.Purchases;
using MyRoad.Infrastructure.Identity.Entities;

namespace MyRoad.Infrastructure.Purchases
{
    public class PurchaseConfiguration : IEntityTypeConfiguration<Purchase>
    {
        public void Configure(EntityTypeBuilder<Purchase> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.PurchasesDate)
                .IsRequired();

            builder.Property(x => x.GoodsDeliverer)
                .HasMaxLength(50).IsRequired();

            builder.Property(x => x.GoodsDelivererPhoneNumber)
                .HasMaxLength(15).IsRequired();

            builder.Property(x => x.Quantity)
                .IsRequired();

            builder.Property(x => x.Price)
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.Property(x => x.Notes)
                .HasMaxLength(500);

            builder.Property(x => x.SupplierId)
                .IsRequired();

            builder.Ignore(x => x.TotalDueAmount);

            builder.HasOne<ApplicationUser>()
               .WithMany()
               .HasForeignKey(x => x.CreatedByUserId)
               .IsRequired();

            builder.HasOne(x => x.Supplier)
               .WithMany(e => e.Purchases)
               .HasForeignKey(x => x.SupplierId);

            builder.ToTable("Purchase");
        }
    }
}
