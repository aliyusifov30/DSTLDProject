using Microsoft.EntityFrameworkCore.Migrations;

namespace DSLTD.Migrations
{
    public partial class ProductCommentUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductComments_AspNetUsers_AppUserId1",
                table: "ProductComments");

            migrationBuilder.DropIndex(
                name: "IX_ProductComments_AppUserId1",
                table: "ProductComments");

            migrationBuilder.DropColumn(
                name: "AppUserId1",
                table: "ProductComments");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "ProductComments",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_ProductComments_AppUserId",
                table: "ProductComments",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductComments_AspNetUsers_AppUserId",
                table: "ProductComments",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductComments_AspNetUsers_AppUserId",
                table: "ProductComments");

            migrationBuilder.DropIndex(
                name: "IX_ProductComments_AppUserId",
                table: "ProductComments");

            migrationBuilder.AlterColumn<int>(
                name: "AppUserId",
                table: "ProductComments",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppUserId1",
                table: "ProductComments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductComments_AppUserId1",
                table: "ProductComments",
                column: "AppUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductComments_AspNetUsers_AppUserId1",
                table: "ProductComments",
                column: "AppUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
