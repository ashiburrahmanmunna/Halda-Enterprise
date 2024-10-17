using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Halda.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate43 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "employeefamilynomineeinfos",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    firstname = table.Column<string>(type: "text", nullable: false),
                    lastname = table.Column<string>(type: "text", nullable: false),
                    occupation = table.Column<string>(type: "text", nullable: false),
                    dob = table.Column<DateOnly>(type: "date", nullable: false),
                    relationship = table.Column<string>(type: "text", nullable: false),
                    alive = table.Column<bool>(type: "boolean", nullable: false),
                    age = table.Column<int>(type: "integer", nullable: false),
                    photopath = table.Column<string>(type: "text", nullable: true),
                    attachmentpath = table.Column<string>(type: "text", nullable: true),
                    infotype = table.Column<int>(type: "integer", nullable: false),
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
                    table.PrimaryKey("pk_employeefamilynomineeinfos", x => x.id);
                    table.ForeignKey(
                        name: "fk_employeefamilynomineeinfos_companys_companyid",
                        column: x => x.companyid,
                        principalTable: "companys",
                        principalColumn: "comid");
                    table.ForeignKey(
                        name: "fk_employeefamilynomineeinfos_employees_employeeid",
                        column: x => x.employeeid,
                        principalTable: "employees",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_employeefamilynomineeinfos_companyid",
                table: "employeefamilynomineeinfos",
                column: "companyid");

            migrationBuilder.CreateIndex(
                name: "ix_employeefamilynomineeinfos_employeeid",
                table: "employeefamilynomineeinfos",
                column: "employeeid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "employeefamilynomineeinfos");
        }
    }
}
