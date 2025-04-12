using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyRoad.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class removeSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "IsActive", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Role", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { 1L, 0, "f451b48d-197e-46ef-ac51-b99e3ad52098", "abdullmen2002@gmail.com", true, "Abdullmen", true, "Fayez", false, null, "ABDULLMEN2002@GMAIL.COM", "ABDULLMEN2002@GMAIL.COM", "AQAAAAIAAYagAAAAED8KM8araMERdjjwlt6NMmD6cHKyH7/ay7ZrnLzjbO4oJ3ioEflWtG+yheG0rZwBhA==", "0123456789", false, "SuperAdmin", null, false, "Abdullmen" });
        }
    }
}
