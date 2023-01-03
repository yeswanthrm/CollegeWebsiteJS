using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CollegeWebsiteAPI.Migrations
{
    public partial class FilesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CasteCertificate",
                table: "Registrations",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "PUCCertificate",
                table: "Registrations",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "SSCCertificate",
                table: "Registrations",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CasteCertificate",
                table: "Registrations");

            migrationBuilder.DropColumn(
                name: "PUCCertificate",
                table: "Registrations");

            migrationBuilder.DropColumn(
                name: "SSCCertificate",
                table: "Registrations");
        }
    }
}
