using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Halda.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addEmployeeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "employees",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    firstname = table.Column<string>(type: "text", nullable: false),
                    lastname = table.Column<string>(type: "text", nullable: false),
                    fathername = table.Column<string>(type: "text", nullable: false),
                    mothername = table.Column<string>(type: "text", nullable: false),
                    dob = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    gender = table.Column<string>(type: "text", nullable: false),
                    religion = table.Column<string>(type: "text", nullable: false),
                    meritalstatus = table.Column<string>(type: "text", nullable: false),
                    nationality = table.Column<string>(type: "text", nullable: false),
                    nid = table.Column<string>(type: "text", nullable: false),
                    passportnumber = table.Column<string>(type: "text", nullable: false),
                    passportissuedate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    primarymno = table.Column<string>(type: "text", nullable: false),
                    secmno = table.Column<string>(type: "text", nullable: false),
                    emergencycontact = table.Column<string>(type: "text", nullable: false),
                    primaryemail = table.Column<string>(type: "text", nullable: false),
                    salaryprofileid = table.Column<int>(type: "integer", nullable: false),
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
                    table.PrimaryKey("pk_employees", x => x.id);
                    table.ForeignKey(
                        name: "fk_employees_companys_companyid",
                        column: x => x.companyid,
                        principalTable: "companys",
                        principalColumn: "comid");
                });

            migrationBuilder.CreateIndex(
                name: "ix_employees_companyid",
                table: "employees",
                column: "companyid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "employees");
        }
    }
}
