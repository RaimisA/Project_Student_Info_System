using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Student_Info_System.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedRelationsOfStudent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lectures_Students_StudentId",
                table: "Lectures");

            migrationBuilder.DropIndex(
                name: "IX_Lectures_StudentId",
                table: "Lectures");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Lectures");

            migrationBuilder.CreateTable(
                name: "LectureStudent",
                columns: table => new
                {
                    LecturesLectureId = table.Column<int>(type: "int", nullable: false),
                    StudentsStudentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LectureStudent", x => new { x.LecturesLectureId, x.StudentsStudentId });
                    table.ForeignKey(
                        name: "FK_LectureStudent_Lectures_LecturesLectureId",
                        column: x => x.LecturesLectureId,
                        principalTable: "Lectures",
                        principalColumn: "LectureId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LectureStudent_Students_StudentsStudentId",
                        column: x => x.StudentsStudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LectureStudent_StudentsStudentId",
                table: "LectureStudent",
                column: "StudentsStudentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LectureStudent");

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "Lectures",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lectures_StudentId",
                table: "Lectures",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lectures_Students_StudentId",
                table: "Lectures",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId");
        }
    }
}
