using Microsoft.EntityFrameworkCore.Migrations;

namespace DSLTD.Migrations
{
    public partial class LoveCountaddedToProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LoveCount",
                table: "Products",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LoveCount",
                table: "Products");
        }
    }
}
