using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Halda.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class TeamMemberModelUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "employeeteams",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    employeeid = table.Column<string>(type: "text", nullable: false),
                    memberid = table.Column<string>(type: "text", nullable: false),
                    joineddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    isteamhead = table.Column<bool>(type: "boolean", nullable: false),
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
                    table.PrimaryKey("pk_employeeteams", x => x.id);
                    table.ForeignKey(
                        name: "fk_employeeteams_companys_companyid",
                        column: x => x.companyid,
                        principalTable: "companys",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_employeeteams_employees_employeeid",
                        column: x => x.employeeid,
                        principalTable: "employees",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_employeeteams_employees_memberid",
                        column: x => x.memberid,
                        principalTable: "employees",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_employeeteams_companyid",
                table: "employeeteams",
                column: "companyid");

            migrationBuilder.CreateIndex(
                name: "ix_employeeteams_employeeid",
                table: "employeeteams",
                column: "employeeid");

            migrationBuilder.CreateIndex(
                name: "ix_employeeteams_memberid",
                table: "employeeteams",
                column: "memberid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "employeeteams");
        }
    }
}
