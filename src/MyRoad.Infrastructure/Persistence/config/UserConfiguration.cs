using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyRoad.Domain.Identity.Enums;
using MyRoad.Infrastructure.Identity.Entities;

namespace MyRoad.Infrastructure.Persistence.config;

public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.HasData(GetSuperAdminUser());
    }

    private static ApplicationUser GetSuperAdminUser()
    {
        var hasher = new PasswordHasher<ApplicationUser>();

        return new ApplicationUser
        {
            Id = 1,
            UserName = "Abdullmen",
            FirstName = "Abdullmen",
            LastName = "Fayez",
            Role = UserRole.SuperAdmin,
            IsActive = true,
            PhoneNumber = "0123456789",
            NormalizedEmail = "abdullmen2002@gmail.com".ToUpper(),
            NormalizedUserName = "abdullmen2002@gmail.com".ToUpper(),
            Email = "abdullmen2002@gmail.com",
            EmailConfirmed = true,
            PasswordHash = hasher.HashPassword(null, "Admin@123")
        };
    }
}