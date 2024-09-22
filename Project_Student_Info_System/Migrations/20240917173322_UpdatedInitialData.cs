using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Project_Student_Info_System.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
