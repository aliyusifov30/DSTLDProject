using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DSLTD.Migrations
{
    public partial class AddressTableCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AddressLimit",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ModifiedAt = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    AppUserId = table.Column<string>(nullable: true),
                    Addresses = table.Column<string>(maxLength: 100, nullable: true),
                    Aparment = table.Column<string>(maxLength: 100, nullable: true),
                    City = table.Column<string>(maxLength: 100, nullable: true),
                    Country = table.Column<string>(maxLength: 100, nullable: true),
                    State = table.Column<string>(maxLength: 100, nullable: true),
                    ZipCode = table.Column<int>(nullable: false),
                    Phone = table.Column<string>(maxLength: 35, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_AppUserId",
                table: "Addresses",
                column: "AppUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropColumn(
                name: "AddressLimit",
                table: "AspNetUsers");
        }
    }
}
