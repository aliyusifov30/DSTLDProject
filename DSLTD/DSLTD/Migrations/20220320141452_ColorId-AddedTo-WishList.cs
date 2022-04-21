using Microsoft.EntityFrameworkCore.Migrations;

namespace DSLTD.Migrations
{
    public partial class ColorIdAddedToWishList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ColorId",
                table: "WishListItems",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_WishListItems_ColorId",
                table: "WishListItems",
                column: "ColorId");

            migrationBuilder.AddForeignKey(
                name: "FK_WishListItems_Colors_ColorId",
                table: "WishListItems",
                column: "ColorId",
                principalTable: "Colors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WishListItems_Colors_ColorId",
                table: "WishListItems");

            migrationBuilder.DropIndex(
                name: "IX_WishListItems_ColorId",
                table: "WishListItems");

            migrationBuilder.DropColumn(
                name: "ColorId",
                table: "WishListItems");
        }
    }
}
