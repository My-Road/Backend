using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyRoad.Domain.Identity.Enums;

namespace MyRoad.Infrastructure.Persistence.config;

public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.HasData(GetRoles());
    }

    private static IEnumerable<IdentityRole> GetRoles()
    {
        var index = 1;

        return Enum.GetValues<UserRole>().Select(role => role.ToString()).Select(roleName => new IdentityRole() { Name = roleName, Id = (index++).ToString(), NormalizedName = roleName.ToUpper(), }).ToList();
    }
}