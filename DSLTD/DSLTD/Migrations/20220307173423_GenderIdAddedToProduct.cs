using Microsoft.EntityFrameworkCore.Migrations;

namespace DSLTD.Migrations
{
    public partial class GenderIdAddedToProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GenderId",
                table: "Products",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Products_GenderId",
                table: "Products",
                column: "GenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Genders_GenderId",
                table: "Products",
                column: "GenderId",
                principalTable: "Genders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Genders_GenderId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_GenderId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "GenderId",
                table: "Products");
        }
    }
}
