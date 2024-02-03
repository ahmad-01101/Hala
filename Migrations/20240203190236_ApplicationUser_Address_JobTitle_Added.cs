using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hala.Migrations
{
    public partial class ApplicationUser_Address_JobTitle_Added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Attendances",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JobTitle",
                table: "Attendances",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "JobTitle",
                table: "Attendances");
        }
    }
}
