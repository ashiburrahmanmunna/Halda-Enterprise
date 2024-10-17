using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Halda.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ModifyEmployeeTaxtbl2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "acknowledgmentslip",
                table: "employeetaxs",
                type: "date",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "acknowledgmentslip",
                table: "employeetaxs");
        }
    }
}
