using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Architecture.Database.Migrations
{
    public partial class fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Auths_AuthId",
                schema: "Architecture",
                table: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Roles_AuthId",
                schema: "Architecture",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "AuthId",
                schema: "Architecture",
                table: "Roles");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AuthId",
                schema: "Architecture",
                table: "Roles",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Roles_AuthId",
                schema: "Architecture",
                table: "Roles",
                column: "AuthId");

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_Auths_AuthId",
                schema: "Architecture",
                table: "Roles",
                column: "AuthId",
                principalSchema: "Architecture",
                principalTable: "Auths",
                principalColumn: "Id");
        }
    }
}
