using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Student_Info_System.Migrations
{
    /// <inheritdoc />
    public partial class ChangePKsInStudentAndDepartment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentLecture_Departments_DepartmentsDepartmentId",
                table: "DepartmentLecture");

            migrationBuilder.DropForeignKey(
                name: "FK_LectureStudent_Students_StudentsStudentId",
                table: "LectureStudent");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Departments_DepartmentId",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Students",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_DepartmentId",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Departments",
                table: "Departments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DepartmentLecture",
                table: "DepartmentLecture");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "DepartmentsDepartmentId",
                table: "DepartmentLecture");

            migrationBuilder.RenameColumn(
                name: "DepartmentId",
                table: "Students",
                newName: "StudentNumber");

            migrationBuilder.RenameColumn(
                name: "StudentsStudentId",
                table: "LectureStudent",
                newName: "StudentsStudentNumber");

            migrationBuilder.RenameIndex(
                name: "IX_LectureStudent_StudentsStudentId",
                table: "LectureStudent",
                newName: "IX_LectureStudent_StudentsStudentNumber");

            migrationBuilder.RenameColumn(
                name: "Code",
                table: "Departments",
                newName: "DepartmentCode");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Students",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.DropColumn(
                name: "StudentNumber",
                table: "Students");

            migrationBuilder.AddColumn<int>(
                name: "StudentNumber",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "DepartmentCode",
                table: "Students",
                type: "nchar(6)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Lectures",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "DepartmentsDepartmentCode",
                table: "DepartmentLecture",
                type: "nchar(6)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Students",
                table: "Students",
                column: "StudentNumber");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Departments",
                table: "Departments",
                column: "DepartmentCode");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DepartmentLecture",
                table: "DepartmentLecture",
                columns: new[] { "DepartmentsDepartmentCode", "LecturesLectureId" });

            migrationBuilder.CreateIndex(
                name: "IX_Students_DepartmentCode",
                table: "Students",
                column: "DepartmentCode");

            migrationBuilder.CreateIndex(
                name: "IX_Students_StudentNumber",
                table: "Students",
                column: "StudentNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lectures_Name",
                table: "Lectures",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Departments_DepartmentCode",
                table: "Departments",
                column: "DepartmentCode",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentLecture_Departments_DepartmentsDepartmentCode",
                table: "DepartmentLecture",
                column: "DepartmentsDepartmentCode",
                principalTable: "Departments",
                principalColumn: "DepartmentCode",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LectureStudent_Students_StudentsStudentNumber",
                table: "LectureStudent",
                column: "StudentsStudentNumber",
                principalTable: "Students",
                principalColumn: "StudentNumber",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Departments_DepartmentCode",
                table: "Students",
                column: "DepartmentCode",
                principalTable: "Departments",
                principalColumn: "DepartmentCode",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentLecture_Departments_DepartmentsDepartmentCode",
                table: "DepartmentLecture");

            migrationBuilder.DropForeignKey(
                name: "FK_LectureStudent_Students_StudentsStudentNumber",
                table: "LectureStudent");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Departments_DepartmentCode",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Students",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_DepartmentCode",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_StudentNumber",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Lectures_Name",
                table: "Lectures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Departments",
                table: "Departments");

            migrationBuilder.DropIndex(
                name: "IX_Departments_DepartmentCode",
                table: "Departments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DepartmentLecture",
                table: "DepartmentLecture");

            migrationBuilder.DropColumn(
                name: "DepartmentCode",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "DepartmentsDepartmentCode",
                table: "DepartmentLecture");

            migrationBuilder.RenameColumn(
                name: "StudentNumber",
                table: "Students",
                newName: "DepartmentId");

            migrationBuilder.RenameColumn(
                name: "StudentsStudentNumber",
                table: "LectureStudent",
                newName: "StudentsStudentId");

            migrationBuilder.RenameIndex(
                name: "IX_LectureStudent_StudentsStudentNumber",
                table: "LectureStudent",
                newName: "IX_LectureStudent_StudentsStudentId");

            migrationBuilder.RenameColumn(
                name: "DepartmentCode",
                table: "Departments",
                newName: "Code");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentId",
                table: "Students",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "Students",
                type: "int",
                fixedLength: true,
                maxLength: 8,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Lectures",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "Departments",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "DepartmentsDepartmentId",
                table: "DepartmentLecture",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Students",
                table: "Students",
                column: "StudentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Departments",
                table: "Departments",
                column: "DepartmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DepartmentLecture",
                table: "DepartmentLecture",
                columns: new[] { "DepartmentsDepartmentId", "LecturesLectureId" });

            migrationBuilder.CreateIndex(
                name: "IX_Students_DepartmentId",
                table: "Students",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentLecture_Departments_DepartmentsDepartmentId",
                table: "DepartmentLecture",
                column: "DepartmentsDepartmentId",
                principalTable: "Departments",
                principalColumn: "DepartmentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LectureStudent_Students_StudentsStudentId",
                table: "LectureStudent",
                column: "StudentsStudentId",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Departments_DepartmentId",
                table: "Students",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "DepartmentId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
