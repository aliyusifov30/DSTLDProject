using Microsoft.EntityFrameworkCore.Migrations;

namespace DSLTD.Migrations
{
    public partial class ColorIdaddedToOrderItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ColorId",
                table: "OrderItem",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ColorName",
                table: "OrderItem",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_ColorId",
                table: "OrderItem",
                column: "ColorId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Colors_ColorId",
                table: "OrderItem",
                column: "ColorId",
                principalTable: "Colors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Colors_ColorId",
                table: "OrderItem");

            migrationBuilder.DropIndex(
                name: "IX_OrderItem_ColorId",
                table: "OrderItem");

            migrationBuilder.DropColumn(
                name: "ColorId",
                table: "OrderItem");

            migrationBuilder.DropColumn(
                name: "ColorName",
                table: "OrderItem");
        }
    }
}
