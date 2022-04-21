using Microsoft.EntityFrameworkCore.Migrations;

namespace DSLTD.Migrations
{
    public partial class CommentUpdateUserNameProp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "ProductComments",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "ProductComments");
        }
    }
}
