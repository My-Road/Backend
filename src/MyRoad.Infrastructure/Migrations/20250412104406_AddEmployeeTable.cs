using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyRoad.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddEmployeeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "4");

            migrationBuilder.DropColumn(
                name: "TotalDebt",
                table: "Employee");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalPaid",
                table: "Employee",
                type: "decimal(6,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,0)");

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "Employee",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalSalary",
                table: "Employee",
                type: "decimal(6,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "039fd247-3e47-4fce-961a-98d8282f6100", "AQAAAAIAAYagAAAAELL8ZsYa3IrvC6ctQyu8FG54hei104dEjY/UiEaiqqSa8tsLyrp2JOrRdV7VkxO4vQ==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalSalary",
                table: "Employee");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalPaid",
                table: "Employee",
                type: "decimal(3,0)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,2)");

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "Employee",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalDebt",
                table: "Employee",
                type: "decimal(3,0)",
                nullable: false,
                defaultValue: 0m);

        }
    }
}
