using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Halda.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddEmployeeBankModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "phonenumber",
                table: "employeeemergencycontacts",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateTable(
                name: "employeebanks",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    acctype = table.Column<int>(type: "integer", nullable: false),
                    accholdername = table.Column<string>(type: "text", nullable: false),
                    accnumber = table.Column<string>(type: "text", nullable: false),
                    bankname = table.Column<string>(type: "text", nullable: false),
                    routingnumber = table.Column<string>(type: "text", nullable: false),
                    branchname = table.Column<string>(type: "text", nullable: false),
                    expirydate = table.Column<DateOnly>(type: "date", nullable: true),
                    cvv = table.Column<string>(type: "text", nullable: false),
                    employeeid = table.Column<string>(type: "text", nullable: false),
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
                    table.PrimaryKey("pk_employeebanks", x => x.id);
                    table.ForeignKey(
                        name: "fk_employeebanks_companys_companyid",
                        column: x => x.companyid,
                        principalTable: "companys",
                        principalColumn: "comid");
                    table.ForeignKey(
                        name: "fk_employeebanks_employees_employeeid",
                        column: x => x.employeeid,
                        principalTable: "employees",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "employeetaxs",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    taxyear = table.Column<DateOnly>(type: "date", nullable: true),
                    name = table.Column<string>(type: "text", nullable: false),
                    returnsubmit = table.Column<bool>(type: "boolean", nullable: false),
                    submitdate = table.Column<DateOnly>(type: "date", nullable: true),
                    acknowledgmentslip = table.Column<string>(type: "text", nullable: false),
                    taxcertificatereceived = table.Column<bool>(type: "boolean", nullable: false),
                    taxextension = table.Column<bool>(type: "boolean", nullable: false),
                    employeeid = table.Column<string>(type: "text", nullable: false),
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
                    table.PrimaryKey("pk_employeetaxs", x => x.id);
                    table.ForeignKey(
                        name: "fk_employeetaxs_companys_companyid",
                        column: x => x.companyid,
                        principalTable: "companys",
                        principalColumn: "comid");
                    table.ForeignKey(
                        name: "fk_employeetaxs_employees_employeeid",
                        column: x => x.employeeid,
                        principalTable: "employees",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_employeebanks_companyid",
                table: "employeebanks",
                column: "companyid");

            migrationBuilder.CreateIndex(
                name: "ix_employeebanks_employeeid",
                table: "employeebanks",
                column: "employeeid");

            migrationBuilder.CreateIndex(
                name: "ix_employeetaxs_companyid",
                table: "employeetaxs",
                column: "companyid");

            migrationBuilder.CreateIndex(
                name: "ix_employeetaxs_employeeid",
                table: "employeetaxs",
                column: "employeeid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "employeebanks");

            migrationBuilder.DropTable(
                name: "employeetaxs");

            migrationBuilder.AlterColumn<int>(
                name: "phonenumber",
                table: "employeeemergencycontacts",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
