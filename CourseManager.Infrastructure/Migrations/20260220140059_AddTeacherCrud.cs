using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTeacherCrud : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "expertise",
                table: "Teacher",
                newName: "Expertise");

            migrationBuilder.CreateIndex(
                name: "IX_Teacher_Email",
                table: "Teacher",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Teacher_Email",
                table: "Teacher");

            migrationBuilder.RenameColumn(
                name: "Expertise",
                table: "Teacher",
                newName: "expertise");
        }
    }
}
