using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Halda.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class BankDetailsCorrection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "banknumber",
                table: "employeebanks",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "reaccnumber",
                table: "employeebanks",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "banknumber",
                table: "employeebanks");

            migrationBuilder.DropColumn(
                name: "reaccnumber",
                table: "employeebanks");
        }
    }
}
