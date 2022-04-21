using Microsoft.EntityFrameworkCore.Migrations;

namespace DSLTD.Migrations
{
    public partial class GenderIdAddedToBestSaleCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GenderId",
                table: "BestSaleCategories",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BestSaleCategories_GenderId",
                table: "BestSaleCategories",
                column: "GenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_BestSaleCategories_Genders_GenderId",
                table: "BestSaleCategories",
                column: "GenderId",
                principalTable: "Genders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BestSaleCategories_Genders_GenderId",
                table: "BestSaleCategories");

            migrationBuilder.DropIndex(
                name: "IX_BestSaleCategories_GenderId",
                table: "BestSaleCategories");

            migrationBuilder.DropColumn(
                name: "GenderId",
                table: "BestSaleCategories");
        }
    }
}
