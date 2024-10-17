using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Halda.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class documentModelUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "employeedocuments",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    type = table.Column<string>(type: "text", nullable: true),
                    docpath = table.Column<string>(type: "text", nullable: true),
                    employeeid = table.Column<string>(type: "text", nullable: true),
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
                    table.PrimaryKey("pk_employeedocuments", x => x.id);
                    table.ForeignKey(
                        name: "fk_employeedocuments_companys_companyid",
                        column: x => x.companyid,
                        principalTable: "companys",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_employeedocuments_employees_employeeid",
                        column: x => x.employeeid,
                        principalTable: "employees",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_employeedocuments_companyid",
                table: "employeedocuments",
                column: "companyid");

            migrationBuilder.CreateIndex(
                name: "ix_employeedocuments_employeeid",
                table: "employeedocuments",
                column: "employeeid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "employeedocuments");
        }
    }
}
