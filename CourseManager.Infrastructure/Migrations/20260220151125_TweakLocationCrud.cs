using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TweakLocationCrud : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StreetName",
                table: "Locations",
                newName: "Classroom");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Classroom",
                table: "Locations",
                newName: "StreetName");
        }
    }
}
