using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreProject_Raihan.Data.Migrations
{
    public partial class arman01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UrlImage",
                table: "Students",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UrlImage",
                table: "Students");
        }
    }
}
