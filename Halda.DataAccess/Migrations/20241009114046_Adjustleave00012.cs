using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Halda.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Adjustleave00012 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "empleaveadjusts",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    empid = table.Column<string>(type: "text", nullable: false),
                    adjusttype = table.Column<string>(type: "text", nullable: false),
                    adjustleaveid = table.Column<string>(type: "text", nullable: false),
                    adjustdate = table.Column<DateOnly>(type: "date", nullable: false),
                    replacedate = table.Column<DateOnly>(type: "date", nullable: false),
                    remarks = table.Column<string>(type: "text", nullable: false),
                    status = table.Column<string>(type: "text", nullable: false),
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
                    table.PrimaryKey("pk_empleaveadjusts", x => x.id);
                    table.ForeignKey(
                        name: "fk_empleaveadjusts_companys_companyid",
                        column: x => x.companyid,
                        principalTable: "companys",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_empleaveadjusts_companyid",
                table: "empleaveadjusts",
                column: "companyid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "empleaveadjusts");
        }
    }
}
