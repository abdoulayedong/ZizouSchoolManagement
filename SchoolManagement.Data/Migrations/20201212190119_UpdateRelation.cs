using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolManagement.Data.Migrations
{
    public partial class UpdateRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Absences_Courses_CourseId",
                table: "Absences");

            migrationBuilder.DropForeignKey(
                name: "FK_Absences_Students_StudentId",
                table: "Absences");

            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Departments_DepartmentId",
                table: "Classes");

            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Professors_HeadDeparmentId",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_Professors_Departments_DepartmentId",
                table: "Professors");

            migrationBuilder.DropTable(
                name: "ClassCourses");

            migrationBuilder.DropIndex(
                name: "IX_Professors_DepartmentId",
                table: "Professors");

            migrationBuilder.DropIndex(
                name: "IX_Departments_HeadDeparmentId",
                table: "Departments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Absences",
                table: "Absences");

            migrationBuilder.DropColumn(
                name: "IsHead",
                table: "Professors");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Professors");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Professors");

            migrationBuilder.DropColumn(
                name: "HeadDeparmentId",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "AbsenceDate",
                table: "Absences");

            migrationBuilder.RenameTable(
                name: "Absences",
                newName: "StudentCourses");

            migrationBuilder.RenameIndex(
                name: "IX_Absences_StudentId",
                table: "StudentCourses",
                newName: "IX_StudentCourses_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_Absences_CourseId",
                table: "StudentCourses",
                newName: "IX_StudentCourses_CourseId");

            migrationBuilder.AddColumn<int>(
                name: "ClassId",
                table: "Students",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPrincipal",
                table: "ProfessorClasses",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Departments",
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentId",
                table: "Classes",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<DateTime>(
                name: "CourseDate",
                table: "StudentCourses",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsPresent",
                table: "StudentCourses",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentCourses",
                table: "StudentCourses",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "CourseClasses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassId = table.Column<int>(nullable: false),
                    CourseId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseClasses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseClasses_Classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseClasses_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProfessorCourses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<int>(nullable: false),
                    ProfessorId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfessorCourses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfessorCourses_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfessorCourses_Professors_ProfessorId",
                        column: x => x.ProfessorId,
                        principalTable: "Professors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProfessorDepartments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProfessorId = table.Column<int>(nullable: false),
                    DepartmentId = table.Column<int>(nullable: false),
                    IsHead = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfessorDepartments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfessorDepartments_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfessorDepartments_Professors_ProfessorId",
                        column: x => x.ProfessorId,
                        principalTable: "Professors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_ClassId",
                table: "Students",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseClasses_ClassId",
                table: "CourseClasses",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseClasses_CourseId",
                table: "CourseClasses",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfessorCourses_CourseId",
                table: "ProfessorCourses",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfessorCourses_ProfessorId",
                table: "ProfessorCourses",
                column: "ProfessorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfessorDepartments_DepartmentId",
                table: "ProfessorDepartments",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfessorDepartments_ProfessorId",
                table: "ProfessorDepartments",
                column: "ProfessorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Departments_DepartmentId",
                table: "Classes",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentCourses_Courses_CourseId",
                table: "StudentCourses",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentCourses_Students_StudentId",
                table: "StudentCourses",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Classes_ClassId",
                table: "Students",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Departments_DepartmentId",
                table: "Classes");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentCourses_Courses_CourseId",
                table: "StudentCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentCourses_Students_StudentId",
                table: "StudentCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Classes_ClassId",
                table: "Students");

            migrationBuilder.DropTable(
                name: "CourseClasses");

            migrationBuilder.DropTable(
                name: "ProfessorCourses");

            migrationBuilder.DropTable(
                name: "ProfessorDepartments");

            migrationBuilder.DropIndex(
                name: "IX_Students_ClassId",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentCourses",
                table: "StudentCourses");

            migrationBuilder.DropColumn(
                name: "ClassId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "IsPrincipal",
                table: "ProfessorClasses");

            migrationBuilder.DropColumn(
                name: "CourseDate",
                table: "StudentCourses");

            migrationBuilder.DropColumn(
                name: "IsPresent",
                table: "StudentCourses");

            migrationBuilder.RenameTable(
                name: "StudentCourses",
                newName: "Absences");

            migrationBuilder.RenameIndex(
                name: "IX_StudentCourses_StudentId",
                table: "Absences",
                newName: "IX_Absences_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentCourses_CourseId",
                table: "Absences",
                newName: "IX_Absences_CourseId");

            migrationBuilder.AddColumn<bool>(
                name: "IsHead",
                table: "Professors",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "Professors",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Professors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Departments",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 5);

            migrationBuilder.AddColumn<int>(
                name: "HeadDeparmentId",
                table: "Departments",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentId",
                table: "Classes",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AbsenceDate",
                table: "Absences",
                type: "DateTime2",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Absences",
                table: "Absences",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ClassCourses",
                columns: table => new
                {
                    ClassId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassCourses", x => new { x.ClassId, x.CourseId });
                    table.ForeignKey(
                        name: "FK_ClassCourses_Classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassCourses_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Professors_DepartmentId",
                table: "Professors",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_HeadDeparmentId",
                table: "Departments",
                column: "HeadDeparmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassCourses_CourseId",
                table: "ClassCourses",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Absences_Courses_CourseId",
                table: "Absences",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Absences_Students_StudentId",
                table: "Absences",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Departments_DepartmentId",
                table: "Classes",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Professors_HeadDeparmentId",
                table: "Departments",
                column: "HeadDeparmentId",
                principalTable: "Professors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Professors_Departments_DepartmentId",
                table: "Professors",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
