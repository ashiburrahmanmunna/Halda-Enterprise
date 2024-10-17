using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Halda.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class empedu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "empedus",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    subjectname = table.Column<string>(type: "text", nullable: false),
                    institute = table.Column<string>(type: "text", nullable: false),
                    startyear = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    endyear = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    grade = table.Column<string>(type: "text", nullable: false),
                    certificationtype = table.Column<int>(type: "integer", nullable: false),
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
                    table.PrimaryKey("pk_empedus", x => x.id);
                    table.ForeignKey(
                        name: "fk_empedus_companys_companyid",
                        column: x => x.companyid,
                        principalTable: "companys",
                        principalColumn: "comid");
                    table.ForeignKey(
                        name: "fk_empedus_employees_employeeid",
                        column: x => x.employeeid,
                        principalTable: "employees",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_empedus_companyid",
                table: "empedus",
                column: "companyid");

            migrationBuilder.CreateIndex(
                name: "ix_empedus_employeeid",
                table: "empedus",
                column: "employeeid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "empedus");
        }
    }
}
