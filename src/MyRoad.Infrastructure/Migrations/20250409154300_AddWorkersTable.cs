using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyRoad.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddWorkersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Workers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    JobTitle = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    DailySalary = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    TotalDebt = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    TotalPaid = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workers", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4", null, "Unknown", "UNKNOWN" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "6b06c1a2-6fa1-4526-a529-63b3a9eebe84", "AQAAAAIAAYagAAAAELiP7luCyJpSB43DaELm8cpmS8TzjBqiPjo2LPQEhCjVl6z3nEebOL+ZZafKXFUYNg==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Workers");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "4");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f451b48d-197e-46ef-ac51-b99e3ad52098", "AQAAAAIAAYagAAAAED8KM8araMERdjjwlt6NMmD6cHKyH7/ay7ZrnLzjbO4oJ3ioEflWtG+yheG0rZwBhA==" });
        }
    }
}
