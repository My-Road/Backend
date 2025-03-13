using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyRoad.Domain.Users;

namespace MyRoad.Infrastructure.Persistence.config;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasColumnName("UserId")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(x => x.Email)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Username)
            .HasMaxLength(10)
            .IsRequired();

        builder.Property(x => x.PhoneNumber)
            .HasMaxLength(14)
            .IsRequired();
    }
}