using Microsoft.EntityFrameworkCore.Migrations;

namespace DSLTD.Migrations
{
    public partial class SizeIdAddedToBasketITem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SizeId",
                table: "BasketItems",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BasketItems_SizeId",
                table: "BasketItems",
                column: "SizeId");

            migrationBuilder.AddForeignKey(
                name: "FK_BasketItems_Sizes_SizeId",
                table: "BasketItems",
                column: "SizeId",
                principalTable: "Sizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketItems_Sizes_SizeId",
                table: "BasketItems");

            migrationBuilder.DropIndex(
                name: "IX_BasketItems_SizeId",
                table: "BasketItems");

            migrationBuilder.DropColumn(
                name: "SizeId",
                table: "BasketItems");
        }
    }
}
