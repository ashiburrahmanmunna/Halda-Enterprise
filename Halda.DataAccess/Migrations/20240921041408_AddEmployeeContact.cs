using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Halda.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddEmployeeContact : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "infotype",
                table: "employeeaddresses");

            migrationBuilder.AddColumn<int>(
                name: "addresstype",
                table: "employeeaddresses",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "employeeemergencycontacts",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    contacttype = table.Column<int>(type: "integer", nullable: false),
                    firstname = table.Column<string>(type: "text", nullable: false),
                    lastname = table.Column<string>(type: "text", nullable: false),
                    relationship = table.Column<string>(type: "text", nullable: false),
                    phonenumber = table.Column<int>(type: "integer", nullable: false),
                    occupation = table.Column<string>(type: "text", nullable: false),
                    officeaddress = table.Column<string>(type: "text", nullable: false),
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
                    table.PrimaryKey("pk_employeeemergencycontacts", x => x.id);
                    table.ForeignKey(
                        name: "fk_employeeemergencycontacts_companys_companyid",
                        column: x => x.companyid,
                        principalTable: "companys",
                        principalColumn: "comid");
                    table.ForeignKey(
                        name: "fk_employeeemergencycontacts_employees_employeeid",
                        column: x => x.employeeid,
                        principalTable: "employees",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_employeeemergencycontacts_companyid",
                table: "employeeemergencycontacts",
                column: "companyid");

            migrationBuilder.CreateIndex(
                name: "ix_employeeemergencycontacts_employeeid",
                table: "employeeemergencycontacts",
                column: "employeeid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "employeeemergencycontacts");

            migrationBuilder.DropColumn(
                name: "addresstype",
                table: "employeeaddresses");

            migrationBuilder.AddColumn<string>(
                name: "infotype",
                table: "employeeaddresses",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
