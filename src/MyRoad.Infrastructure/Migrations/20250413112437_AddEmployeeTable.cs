using System;
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
            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Employee");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "Employee",
                newName: "EndDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "Employee",
                newName: "CreatedOn");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Employee",
                type: "datetime2",
                nullable: true);
        }
    }
}
