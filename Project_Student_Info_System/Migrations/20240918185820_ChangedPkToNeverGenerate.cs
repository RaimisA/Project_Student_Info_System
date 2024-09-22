using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Project_Student_Info_System.Migrations
{
    /// <inheritdoc />
    public partial class ChangedPkToNeverGenerate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Delete existing data to avoid foreign key constraints issues
            migrationBuilder.DeleteData(
                table: "DepartmentLecture",
                keyColumns: new[] { "DepartmentCode", "LectureId" },
                keyValues: new object[] { "CS0050", 1 });

            migrationBuilder.DeleteData(
                table: "DepartmentLecture",
                keyColumns: new[] { "DepartmentCode", "LectureId" },
                keyValues: new object[] { "CS0050", 3 });

            migrationBuilder.DeleteData(
                table: "DepartmentLecture",
                keyColumns: new[] { "DepartmentCode", "LectureId" },
                keyValues: new object[] { "MATH20", 2 });

            migrationBuilder.DeleteData(
                table: "DepartmentLecture",
                keyColumns: new[] { "DepartmentCode", "LectureId" },
                keyValues: new object[] { "PHYS30", 3 });

            migrationBuilder.DeleteData(
                table: "LectureStudent",
                keyColumns: new[] { "LectureId", "StudentNumber" },
                keyValues: new object[] { 1, 10001001 });

            migrationBuilder.DeleteData(
                table: "LectureStudent",
                keyColumns: new[] { "LectureId", "StudentNumber" },
                keyValues: new object[] { 1, 10001003 });

            migrationBuilder.DeleteData(
                table: "LectureStudent",
                keyColumns: new[] { "LectureId", "StudentNumber" },
                keyValues: new object[] { 2, 10001001 });

            migrationBuilder.DeleteData(
                table: "LectureStudent",
                keyColumns: new[] { "LectureId", "StudentNumber" },
                keyValues: new object[] { 2, 10001002 });

            migrationBuilder.DeleteData(
                table: "LectureStudent",
                keyColumns: new[] { "LectureId", "StudentNumber" },
                keyValues: new object[] { 3, 10001002 });

            migrationBuilder.DeleteData(
                table: "LectureStudent",
                keyColumns: new[] { "LectureId", "StudentNumber" },
                keyValues: new object[] { 3, 10001003 });

            migrationBuilder.DeleteData(
                table: "Lectures",
                keyColumn: "LectureId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Lectures",
                keyColumn: "LectureId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Lectures",
                keyColumn: "LectureId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "StudentNumber",
                keyValue: 10001001);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "StudentNumber",
                keyValue: 10001002);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "StudentNumber",
                keyValue: 10001003);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "DepartmentCode",
                keyValue: "CS0050");

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "DepartmentCode",
                keyValue: "MATH20");

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "DepartmentCode",
                keyValue: "PHYS30");

            // Drop foreign key constraints
            migrationBuilder.DropForeignKey(
                name: "FK_LectureStudent_Students_StudentNumber",
                table: "LectureStudent");

            // Drop primary key constraint
            migrationBuilder.DropPrimaryKey(
                name: "PK_Students",
                table: "Students");

            // Drop index
            migrationBuilder.DropIndex(
                name: "IX_Students_StudentNumber",
                table: "Students");

            // Drop the existing StudentNumber column
            migrationBuilder.DropColumn(
                name: "StudentNumber",
                table: "Students");

            // Add the StudentNumber column back without the IDENTITY property
            migrationBuilder.AddColumn<int>(
                name: "StudentNumber",
                table: "Students",
                nullable: false);

            // Recreate the primary key constraint
            migrationBuilder.AddPrimaryKey(
                name: "PK_Students",
                table: "Students",
                column: "StudentNumber");

            // Recreate the index
            migrationBuilder.CreateIndex(
                name: "IX_Students_StudentNumber",
                table: "Students",
                column: "StudentNumber",
                unique: true);

            // Recreate foreign key constraints
            migrationBuilder.AddForeignKey(
                name: "FK_LectureStudent_Students_StudentNumber",
                table: "LectureStudent",
                column: "StudentNumber",
                principalTable: "Students",
                principalColumn: "StudentNumber",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop foreign key constraints
            migrationBuilder.DropForeignKey(
                name: "FK_LectureStudent_Students_StudentNumber",
                table: "LectureStudent");

            // Drop primary key constraint
            migrationBuilder.DropPrimaryKey(
                name: "PK_Students",
                table: "Students");

            // Drop index
            migrationBuilder.DropIndex(
                name: "IX_Students_StudentNumber",
                table: "Students");

            // Drop the StudentNumber column
            migrationBuilder.DropColumn(
                name: "StudentNumber",
                table: "Students");

            // Add the StudentNumber column back with the IDENTITY property
            migrationBuilder.AddColumn<int>(
                name: "StudentNumber",
                table: "Students",
                nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");

            // Recreate the primary key constraint
            migrationBuilder.AddPrimaryKey(
                name: "PK_Students",
                table: "Students",
                column: "StudentNumber");

            // Recreate the index
            migrationBuilder.CreateIndex(
                name: "IX_Students_StudentNumber",
                table: "Students",
                column: "StudentNumber",
                unique: true);

            // Recreate foreign key constraints
            migrationBuilder.AddForeignKey(
                name: "FK_LectureStudent_Students_StudentNumber",
                table: "LectureStudent",
                column: "StudentNumber",
                principalTable: "Students",
                principalColumn: "StudentNumber",
                onDelete: ReferentialAction.Cascade);

            // Reinsert the deleted data
            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "DepartmentCode", "Name" },
                values: new object[,]
                {
                    { "CS0050", "Computer Science" },
                    { "MATH20", "Mathematics" },
                    { "PHYS30", "Physics" }
                });

            migrationBuilder.InsertData(
                table: "Lectures",
                columns: new[] { "LectureId", "EndTime", "Name", "StartTime", "WeekDay" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 9, 17, 10, 30, 0, 0, DateTimeKind.Unspecified), "Materials Science", new DateTime(2024, 9, 17, 9, 0, 0, 0, DateTimeKind.Unspecified), "Monday" },
                    { 2, new DateTime(2024, 9, 17, 12, 30, 0, 0, DateTimeKind.Unspecified), "Machine Design", new DateTime(2024, 9, 17, 11, 0, 0, 0, DateTimeKind.Unspecified), "Tuesday" },
                    { 3, new DateTime(2024, 9, 17, 15, 30, 0, 0, DateTimeKind.Unspecified), "Thermodynamics", new DateTime(2024, 9, 17, 14, 0, 0, 0, DateTimeKind.Unspecified), "Wednesday" }
                });

            migrationBuilder.InsertData(
                table: "DepartmentLecture",
                columns: new[] { "DepartmentCode", "LectureId" },
                values: new object[,]
                {
                    { "CS0050", 1 },
                    { "CS0050", 3 },
                    { "MATH20", 2 },
                    { "PHYS30", 3 }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "StudentNumber", "DepartmentCode", "Email", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 10001001, "CS0050", "ansel.reed@example.com", "Ansel", "Reed" },
                    { 10001002, "MATH20", "greta.hale@example.com", "Greta", "Hale" },
                    { 10001003, "PHYS30", "silas.lang@example.com", "Silas", "Lang" }
                });

            migrationBuilder.InsertData(
                table: "LectureStudent",
                columns: new[] { "LectureId", "StudentNumber" },
                values: new object[,]
                {
                    { 1, 10001001 },
                    { 1, 10001003 },
                    { 2, 10001001 },
                    { 2, 10001002 },
                    { 3, 10001002 },
                    { 3, 10001003 }
                });
        }
    }
}
