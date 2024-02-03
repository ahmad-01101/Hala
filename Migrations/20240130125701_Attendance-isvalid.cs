using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hala.Migrations
{
    public partial class Attendanceisvalid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Isvalid",
                table: "Attendances",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Isvalid",
                table: "Attendances");
        }
    }
}
