using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Architecture.Database.Migrations
{
    public partial class TreeFlateParent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema("Architecture");
            migrationBuilder.Sql(@"
            CREATE OR REPLACE VIEW ""Architecture"".""FlateParentTrees""
            AS
                SELECT row_number() OVER () AS ""Id"",
            ff.""TreeId"",
            ff.""ParentId"",
            ff.""GrandParentId""
            FROM(
                WITH RECURSIVE ""FlateTrees""(""Id"", ""TreeId"", ""ParentId"", ""GrandParentId"") AS (
                    SELECT ""Trees"".""Id"",
                    ""Trees"".""Id"" AS ""TreeId"",
                    ""Trees"".""ParentId"",
                    ""Trees"".""Id"" AS ""GrandParentId""
            FROM ""Architecture"".""Trees""
            WHERE ""Trees"".""ParentId"" IS NULL or
            (""Trees"".""ParentId"" IN
                (SELECT ""Trees_1"".""Id""
            FROM ""Architecture"".""Trees"" ""Trees_1"")
                )
            UNION ALL
            SELECT p.""Id"",
            p.""Id"" AS ""TreeId"",
            p.""ParentId"",
            f.""GrandParentId""
            FROM ""FlateTrees"" f
                JOIN ""Architecture"".""Trees"" p ON f.""Id"" = p.""ParentId"")
            SELECT fr.""Id"",
            fr.""Id"" AS ""TreeId"",
            fr.""GrandParentId"" as ""ParentId"",
            pr.""ParentId"" as ""GrandParentId""
            FROM ""FlateTrees"" fr
                JOIN ""Architecture"".""Trees"" pr on fr.""GrandParentId"" = pr.""Id""
            order by fr.""ParentId"") ff
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema("Architecture");
            migrationBuilder.Sql(@"drop view ""Architecture"".""FlateParentTrees""");
        }
    }
}
