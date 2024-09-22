using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Project_Student_Info_System.Migrations
{
    /// <inheritdoc />
    public partial class AddInitialDataToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentLecture_Departments_DepartmentsDepartmentCode",
                table: "DepartmentLecture");

            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentLecture_Lectures_LecturesLectureId",
                table: "DepartmentLecture");

            migrationBuilder.DropForeignKey(
                name: "FK_LectureStudent_Lectures_LecturesLectureId",
                table: "LectureStudent");

            migrationBuilder.DropForeignKey(
                name: "FK_LectureStudent_Students_StudentsStudentNumber",
                table: "LectureStudent");

            migrationBuilder.RenameColumn(
                name: "StudentsStudentNumber",
                table: "LectureStudent",
                newName: "StudentNumber");

            migrationBuilder.RenameColumn(
                name: "LecturesLectureId",
                table: "LectureStudent",
                newName: "LectureId");

            migrationBuilder.RenameIndex(
                name: "IX_LectureStudent_StudentsStudentNumber",
                table: "LectureStudent",
                newName: "IX_LectureStudent_StudentNumber");

            migrationBuilder.RenameColumn(
                name: "LecturesLectureId",
                table: "DepartmentLecture",
                newName: "LectureId");

            migrationBuilder.RenameColumn(
                name: "DepartmentsDepartmentCode",
                table: "DepartmentLecture",
                newName: "DepartmentCode");

            migrationBuilder.RenameIndex(
                name: "IX_DepartmentLecture_LecturesLectureId",
                table: "DepartmentLecture",
                newName: "IX_DepartmentLecture_LectureId");

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
                    { 1001, "CS0050", "ansel.reed@example.com", "Ansel", "Reed" },
                    { 1002, "MATH20", "greta.hale@example.com", "Greta", "Hale" },
                    { 1003, "PHYS30", "silas.lang@example.com", "Silas", "Lang" }
                });

            migrationBuilder.InsertData(
                table: "LectureStudent",
                columns: new[] { "LectureId", "StudentNumber" },
                values: new object[,]
                {
                    { 1, 1001 },
                    { 1, 1003 },
                    { 2, 1001 },
                    { 2, 1002 },
                    { 3, 1002 },
                    { 3, 1003 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentLecture_Departments_DepartmentCode",
                table: "DepartmentLecture",
                column: "DepartmentCode",
                principalTable: "Departments",
                principalColumn: "DepartmentCode",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentLecture_Lectures_LectureId",
                table: "DepartmentLecture",
                column: "LectureId",
                principalTable: "Lectures",
                principalColumn: "LectureId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LectureStudent_Lectures_LectureId",
                table: "LectureStudent",
                column: "LectureId",
                principalTable: "Lectures",
                principalColumn: "LectureId",
                onDelete: ReferentialAction.Cascade);

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
            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentLecture_Departments_DepartmentCode",
                table: "DepartmentLecture");

            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentLecture_Lectures_LectureId",
                table: "DepartmentLecture");

            migrationBuilder.DropForeignKey(
                name: "FK_LectureStudent_Lectures_LectureId",
                table: "LectureStudent");

            migrationBuilder.DropForeignKey(
                name: "FK_LectureStudent_Students_StudentNumber",
                table: "LectureStudent");

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
                keyValues: new object[] { 1, 1001 });

            migrationBuilder.DeleteData(
                table: "LectureStudent",
                keyColumns: new[] { "LectureId", "StudentNumber" },
                keyValues: new object[] { 1, 1003 });

            migrationBuilder.DeleteData(
                table: "LectureStudent",
                keyColumns: new[] { "LectureId", "StudentNumber" },
                keyValues: new object[] { 2, 1001 });

            migrationBuilder.DeleteData(
                table: "LectureStudent",
                keyColumns: new[] { "LectureId", "StudentNumber" },
                keyValues: new object[] { 2, 1002 });

            migrationBuilder.DeleteData(
                table: "LectureStudent",
                keyColumns: new[] { "LectureId", "StudentNumber" },
                keyValues: new object[] { 3, 1002 });

            migrationBuilder.DeleteData(
                table: "LectureStudent",
                keyColumns: new[] { "LectureId", "StudentNumber" },
                keyValues: new object[] { 3, 1003 });

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
                keyValue: 1001);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "StudentNumber",
                keyValue: 1002);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "StudentNumber",
                keyValue: 1003);

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

            migrationBuilder.RenameColumn(
                name: "StudentNumber",
                table: "LectureStudent",
                newName: "StudentsStudentNumber");

            migrationBuilder.RenameColumn(
                name: "LectureId",
                table: "LectureStudent",
                newName: "LecturesLectureId");

            migrationBuilder.RenameIndex(
                name: "IX_LectureStudent_StudentNumber",
                table: "LectureStudent",
                newName: "IX_LectureStudent_StudentsStudentNumber");

            migrationBuilder.RenameColumn(
                name: "LectureId",
                table: "DepartmentLecture",
                newName: "LecturesLectureId");

            migrationBuilder.RenameColumn(
                name: "DepartmentCode",
                table: "DepartmentLecture",
                newName: "DepartmentsDepartmentCode");

            migrationBuilder.RenameIndex(
                name: "IX_DepartmentLecture_LectureId",
                table: "DepartmentLecture",
                newName: "IX_DepartmentLecture_LecturesLectureId");

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentLecture_Departments_DepartmentsDepartmentCode",
                table: "DepartmentLecture",
                column: "DepartmentsDepartmentCode",
                principalTable: "Departments",
                principalColumn: "DepartmentCode",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentLecture_Lectures_LecturesLectureId",
                table: "DepartmentLecture",
                column: "LecturesLectureId",
                principalTable: "Lectures",
                principalColumn: "LectureId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LectureStudent_Lectures_LecturesLectureId",
                table: "LectureStudent",
                column: "LecturesLectureId",
                principalTable: "Lectures",
                principalColumn: "LectureId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LectureStudent_Students_StudentsStudentNumber",
                table: "LectureStudent",
                column: "StudentsStudentNumber",
                principalTable: "Students",
                principalColumn: "StudentNumber",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
