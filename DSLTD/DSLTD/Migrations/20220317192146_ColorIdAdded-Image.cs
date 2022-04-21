using Microsoft.EntityFrameworkCore.Migrations;

namespace DSLTD.Migrations
{
    public partial class ColorIdAddedImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ColorId",
                table: "ProductsImages",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProductsImages_ColorId",
                table: "ProductsImages",
                column: "ColorId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsImages_Colors_ColorId",
                table: "ProductsImages",
                column: "ColorId",
                principalTable: "Colors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductsImages_Colors_ColorId",
                table: "ProductsImages");

            migrationBuilder.DropIndex(
                name: "IX_ProductsImages_ColorId",
                table: "ProductsImages");

            migrationBuilder.DropColumn(
                name: "ColorId",
                table: "ProductsImages");
        }
    }
}
