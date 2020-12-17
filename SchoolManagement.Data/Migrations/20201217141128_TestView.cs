using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolManagement.Data.Migrations
{
    public partial class TestView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql = @"CREATE OR ALTER VIEW [dbo].[vwDepartmentProfessor] As
                        Select pd.Id, p.Id As ProfessorId, d.Id As DepartmentId, p.FirstName, p.LastName, d.Name, d.Code, pd.IsHead
                        From ProfessorDepartments pd
                        Join Professors p On p.Id = pd.professorId
                        Join Departments d On d.Id = pd.DepartmentId
                ";
            migrationBuilder.Sql(sql);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP VIEW vwDepartmentProfessor");
        }
    }
}
