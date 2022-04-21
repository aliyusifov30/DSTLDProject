using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DSLTD.Migrations
{
    public partial class ShopOnInstagramTableCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ShopOnInstagrams",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    CreateAt = table.Column<DateTime>(nullable: false),
                    Desc = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopOnInstagrams", x => x.Id);
                });
        }
        
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShopOnInstagrams");
        }

    }
}
