using Microsoft.EntityFrameworkCore.Migrations;

namespace Dictionarium.Migrations.UserModelContextMigrations
{
    public partial class Added_Russian_Collate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase(
                collation: "Cyrillic_General_CI_AS");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase(
                oldCollation: "Cyrillic_General_CI_AS");
        }
    }
}
