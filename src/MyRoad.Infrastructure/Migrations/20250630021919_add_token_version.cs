﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyRoad.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_token_version : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "TokenVersion",
                table: "Users",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TokenVersion",
                table: "Users");
        }
    }
}
