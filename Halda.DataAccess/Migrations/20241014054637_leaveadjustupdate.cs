using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Halda.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class leaveadjustupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "ix_empleaveadjusts_empid",
                table: "empleaveadjusts",
                column: "empid");

            migrationBuilder.AddForeignKey(
                name: "fk_empleaveadjusts_employees_empid",
                table: "empleaveadjusts",
                column: "empid",
                principalTable: "employees",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_empleaveadjusts_employees_empid",
                table: "empleaveadjusts");

            migrationBuilder.DropIndex(
                name: "ix_empleaveadjusts_empid",
                table: "empleaveadjusts");
        }
    }
}
