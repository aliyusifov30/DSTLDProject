using Microsoft.EntityFrameworkCore.Migrations;

namespace DSLTD.Migrations
{
    public partial class BasketItemUpdateFirstTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_BasketItems_ProductId",
                table: "BasketItems",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_BasketItems_Products_ProductId",
                table: "BasketItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketItems_Products_ProductId",
                table: "BasketItems");

            migrationBuilder.DropIndex(
                name: "IX_BasketItems_ProductId",
                table: "BasketItems");
        }
    }
}
