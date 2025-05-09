using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyRoad.Domain.Customers;

namespace MyRoad.Infrastructure.Customers;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.Property(x => x.FullName)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.Email)
            .HasMaxLength(200);

        builder.Property(x => x.PhoneNumber)
            .HasMaxLength(15)
            .IsRequired();

        builder.Property(x => x.Address)
            .HasMaxLength(100);

        builder.Property(x => x.TotalDueAmount)
            .HasColumnType("decimal(10,2)");

        builder.Property(x => x.TotalPaidAmount)
            .HasColumnType("decimal(10,2)");

        builder.HasIndex(x => x.Email)
            .IsUnique()
            .HasFilter("[Email] <> ''");
        
        builder.HasIndex(x => x.PhoneNumber)
            .IsUnique();

        builder.HasMany(x => x.Orders)
            .WithOne(x => x.Customer)
            .HasForeignKey(x => x.CustomerId);

        builder.HasMany(x => x.CustomerPayments)
            .WithOne(x => x.Customer)
            .HasForeignKey(x => x.CustomerId);

        builder.Ignore(x => x.RemainingAmount);

        builder.ToTable("Customer");
    }
}