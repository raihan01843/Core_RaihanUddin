using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreProject_Raihan.Data.Migrations
{
    public partial class ar01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AutherName = table.Column<string>(maxLength: 100, nullable: false),
                    Qualification = table.Column<string>(maxLength: 100, nullable: false),
                    Experience = table.Column<int>(maxLength: 100, nullable: false),
                    WrittenDate = table.Column<DateTime>(nullable: false),
                    PublishDate = table.Column<DateTime>(nullable: false),
                    Venue = table.Column<string>(maxLength: 255, nullable: false),
                    ProfilePicture = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authers", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Authers");
        }
    }
}
