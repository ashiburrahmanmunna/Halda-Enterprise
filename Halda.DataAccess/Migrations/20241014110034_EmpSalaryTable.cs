using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Halda.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class EmpSalaryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "empsalarys",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    employeeid = table.Column<string>(type: "text", nullable: true),
                    grosssalary = table.Column<double>(type: "double precision", nullable: false),
                    minsalary = table.Column<double>(type: "double precision", nullable: false),
                    maxsalary = table.Column<double>(type: "double precision", nullable: false),
                    companyid = table.Column<string>(type: "text", nullable: true),
                    serial = table.Column<int>(type: "integer", nullable: true),
                    userid = table.Column<string>(type: "text", nullable: true),
                    updatebyuserid = table.Column<string>(type: "text", nullable: true),
                    isdelete = table.Column<bool>(type: "boolean", nullable: true),
                    dateadded = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    dateupdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_empsalarys", x => x.id);
                    table.ForeignKey(
                        name: "fk_empsalarys_companys_companyid",
                        column: x => x.companyid,
                        principalTable: "companys",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_empsalarys_employees_employeeid",
                        column: x => x.employeeid,
                        principalTable: "employees",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_empsalarys_companyid",
                table: "empsalarys",
                column: "companyid");

            migrationBuilder.CreateIndex(
                name: "ix_empsalarys_employeeid",
                table: "empsalarys",
                column: "employeeid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "empsalarys");
        }
    }
}
