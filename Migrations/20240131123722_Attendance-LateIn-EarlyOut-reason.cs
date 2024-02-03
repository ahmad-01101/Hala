using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hala.Migrations
{
    public partial class AttendanceLateInEarlyOutreason : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EarlyCheckedOut_reason",
                table: "Attendances",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LateCheckedIn_reason",
                table: "Attendances",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EarlyCheckedOut_reason",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "LateCheckedIn_reason",
                table: "Attendances");
        }
    }
}
