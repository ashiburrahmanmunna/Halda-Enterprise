using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Halda.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class EmpSalaryTable2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "departmentid",
                table: "empsalarys",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "designationid",
                table: "empsalarys",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_empsalarys_departmentid",
                table: "empsalarys",
                column: "departmentid");

            migrationBuilder.CreateIndex(
                name: "ix_empsalarys_designationid",
                table: "empsalarys",
                column: "designationid");

            migrationBuilder.AddForeignKey(
                name: "fk_empsalarys_departments_departmentid",
                table: "empsalarys",
                column: "departmentid",
                principalTable: "departments",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_empsalarys_designations_designationid",
                table: "empsalarys",
                column: "designationid",
                principalTable: "designations",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_empsalarys_departments_departmentid",
                table: "empsalarys");

            migrationBuilder.DropForeignKey(
                name: "fk_empsalarys_designations_designationid",
                table: "empsalarys");

            migrationBuilder.DropIndex(
                name: "ix_empsalarys_departmentid",
                table: "empsalarys");

            migrationBuilder.DropIndex(
                name: "ix_empsalarys_designationid",
                table: "empsalarys");

            migrationBuilder.DropColumn(
                name: "departmentid",
                table: "empsalarys");

            migrationBuilder.DropColumn(
                name: "designationid",
                table: "empsalarys");
        }
    }
}
