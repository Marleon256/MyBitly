using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyBitly.Migrations
{
    public partial class UrlData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UrlDatas",
                columns: table => new
                {
                    ShortUrl = table.Column<string>(nullable: false),
                    FullUrl = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    HopCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UrlDatas", x => x.ShortUrl);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UrlDatas");
        }
    }
}
