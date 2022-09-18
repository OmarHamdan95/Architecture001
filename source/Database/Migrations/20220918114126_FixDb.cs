using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Architecture.Database.Migrations
{
    public partial class FixDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ValidFrom",
                schema: "Architecture",
                table: "Statuses",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ValidTo",
                schema: "Architecture",
                table: "Statuses",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ValidFrom",
                schema: "Architecture",
                table: "Roles",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ValidTo",
                schema: "Architecture",
                table: "Roles",
                type: "timestamp with time zone",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValidFrom",
                schema: "Architecture",
                table: "Statuses");

            migrationBuilder.DropColumn(
                name: "ValidTo",
                schema: "Architecture",
                table: "Statuses");

            migrationBuilder.DropColumn(
                name: "ValidFrom",
                schema: "Architecture",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "ValidTo",
                schema: "Architecture",
                table: "Roles");
        }
    }
}
