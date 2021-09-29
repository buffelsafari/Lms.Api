using Microsoft.EntityFrameworkCore.Migrations;

namespace Lms.Data.Migrations
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Module_Course_courseId",
                table: "Module");

            migrationBuilder.RenameColumn(
                name: "courseId",
                table: "Module",
                newName: "CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_Module_courseId",
                table: "Module",
                newName: "IX_Module_CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Module_Course_CourseId",
                table: "Module",
                column: "CourseId",
                principalTable: "Course",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Module_Course_CourseId",
                table: "Module");

            migrationBuilder.RenameColumn(
                name: "CourseId",
                table: "Module",
                newName: "courseId");

            migrationBuilder.RenameIndex(
                name: "IX_Module_CourseId",
                table: "Module",
                newName: "IX_Module_courseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Module_Course_courseId",
                table: "Module",
                column: "courseId",
                principalTable: "Course",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
