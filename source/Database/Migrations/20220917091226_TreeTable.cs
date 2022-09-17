using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Architecture.Database.Migrations
{
    public partial class TreeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name_NameEn",
                schema: "Architecture",
                table: "Statuses",
                newName: "Description_NameEn");

            migrationBuilder.RenameColumn(
                name: "Name_NameAr",
                schema: "Architecture",
                table: "Statuses",
                newName: "Description_NameAr");

            migrationBuilder.RenameColumn(
                name: "Name_NameEn",
                schema: "Architecture",
                table: "Roles",
                newName: "Description_NameEn");

            migrationBuilder.RenameColumn(
                name: "Name_NameAr",
                schema: "Architecture",
                table: "Roles",
                newName: "Description_NameAr");

            migrationBuilder.CreateTable(
                name: "Trees",
                schema: "Architecture",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description_NameAr = table.Column<string>(type: "text", nullable: true),
                    Description_NameEn = table.Column<string>(type: "text", nullable: true),
                    Code = table.Column<string>(type: "text", nullable: true),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedBy = table.Column<string>(type: "text", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trees_Trees_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "Architecture",
                        principalTable: "Trees",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Trees_ParentId",
                schema: "Architecture",
                table: "Trees",
                column: "ParentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Trees",
                schema: "Architecture");

            migrationBuilder.RenameColumn(
                name: "Description_NameEn",
                schema: "Architecture",
                table: "Statuses",
                newName: "Name_NameEn");

            migrationBuilder.RenameColumn(
                name: "Description_NameAr",
                schema: "Architecture",
                table: "Statuses",
                newName: "Name_NameAr");

            migrationBuilder.RenameColumn(
                name: "Description_NameEn",
                schema: "Architecture",
                table: "Roles",
                newName: "Name_NameEn");

            migrationBuilder.RenameColumn(
                name: "Description_NameAr",
                schema: "Architecture",
                table: "Roles",
                newName: "Name_NameAr");
        }
    }
}
