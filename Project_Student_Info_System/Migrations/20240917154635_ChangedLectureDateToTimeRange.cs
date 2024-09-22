using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Student_Info_System.Migrations
{
    /// <inheritdoc />
    public partial class ChangedLectureDateToTimeRange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Lectures",
                newName: "StartTime");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "Lectures",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "WeekDay",
                table: "Lectures",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Lectures");

            migrationBuilder.DropColumn(
                name: "WeekDay",
                table: "Lectures");

            migrationBuilder.RenameColumn(
                name: "StartTime",
                table: "Lectures",
                newName: "Date");
        }
    }
}
