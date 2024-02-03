using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hala.Migrations
{
    public partial class AttendanceDateOnly_1Added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateOnly_1",
                table: "Attendances",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOnly_1",
                table: "Attendances");
        }
    }
}
