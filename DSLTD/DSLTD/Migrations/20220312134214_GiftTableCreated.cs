using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DSLTD.Migrations
{
    public partial class GiftTableCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GiftId",
                table: "Products",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Gifts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ModifiedAt = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Code = table.Column<string>(maxLength: 10, nullable: true),
                    GiftDiscount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gifts", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_GiftId",
                table: "Products",
                column: "GiftId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Gifts_GiftId",
                table: "Products",
                column: "GiftId",
                principalTable: "Gifts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Gifts_GiftId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Gifts");

            migrationBuilder.DropIndex(
                name: "IX_Products_GiftId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "GiftId",
                table: "Products");
        }
    }
}
