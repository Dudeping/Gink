using Microsoft.EntityFrameworkCore.Migrations;

namespace Codeping.Gink.Web.Migrations
{
    public partial class InitDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Links",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Total = table.Column<int>(nullable: false),
                    LongUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Links", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Links_LongUrl",
                table: "Links",
                column: "LongUrl");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Links");
        }
    }
}
