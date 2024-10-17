using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Halda.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class EmployeeModelCorrect : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "jobtitle",
                table: "employees",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "location",
                table: "employees",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "jobtitle",
                table: "employees");

            migrationBuilder.DropColumn(
                name: "location",
                table: "employees");
        }
    }
}
