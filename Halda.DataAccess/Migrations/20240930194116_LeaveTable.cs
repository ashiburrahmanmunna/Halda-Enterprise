﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Halda.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class LeaveTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "holidays",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    holidaysname = table.Column<string>(type: "text", nullable: false),
                    date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    day = table.Column<string>(type: "text", nullable: false),
                    holidaytype = table.Column<string>(type: "text", nullable: false),
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
                    table.PrimaryKey("pk_holidays", x => x.id);
                    table.ForeignKey(
                        name: "fk_holidays_companys_companyid",
                        column: x => x.companyid,
                        principalTable: "companys",
                        principalColumn: "comid");
                });

            migrationBuilder.CreateTable(
                name: "leaves",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    leavename = table.Column<string>(type: "text", nullable: false),
                    employeestatus = table.Column<string>(type: "text", nullable: false),
                    leaveperperiod = table.Column<int>(type: "integer", nullable: false),
                    employeepreapply = table.Column<bool>(type: "boolean", nullable: false),
                    maximumday = table.Column<int>(type: "integer", nullable: false),
                    futureadjustable = table.Column<bool>(type: "boolean", nullable: false),
                    percentageleavecarriedforward = table.Column<float>(type: "real", nullable: false),
                    eligiblefor = table.Column<string>(type: "text", nullable: false),
                    adminassigntoemployee = table.Column<bool>(type: "boolean", nullable: false),
                    leaveoptions = table.Column<string>(type: "text", nullable: false),
                    paidtimeoff = table.Column<bool>(type: "boolean", nullable: false),
                    withindays = table.Column<int>(type: "integer", nullable: false),
                    maximumhours = table.Column<int>(type: "integer", nullable: false),
                    allowedfor = table.Column<string>(type: "text", nullable: false),
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
                    table.PrimaryKey("pk_leaves", x => x.id);
                    table.ForeignKey(
                        name: "fk_leaves_companys_companyid",
                        column: x => x.companyid,
                        principalTable: "companys",
                        principalColumn: "comid");
                });

            migrationBuilder.CreateTable(
                name: "shifts",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    shiftid = table.Column<int>(type: "integer", nullable: false),
                    shiftname = table.Column<string>(type: "text", nullable: false),
                    shifttype = table.Column<string>(type: "text", nullable: false),
                    shiftin = table.Column<TimeSpan>(type: "interval", nullable: false),
                    shiftout = table.Column<TimeSpan>(type: "interval", nullable: false),
                    breakin = table.Column<TimeSpan>(type: "interval", nullable: true),
                    breakout = table.Column<TimeSpan>(type: "interval", nullable: true),
                    secondbreakin = table.Column<TimeSpan>(type: "interval", nullable: true),
                    secondbreakout = table.Column<TimeSpan>(type: "interval", nullable: true),
                    thirdbreakin = table.Column<TimeSpan>(type: "interval", nullable: true),
                    thirdbreakout = table.Column<TimeSpan>(type: "interval", nullable: true),
                    nightcross = table.Column<bool>(type: "boolean", nullable: false),
                    weekendallowtime = table.Column<TimeSpan>(type: "interval", nullable: true),
                    totalhour = table.Column<TimeSpan>(type: "interval", nullable: false),
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
                    table.PrimaryKey("pk_shifts", x => x.id);
                    table.ForeignKey(
                        name: "fk_shifts_companys_companyid",
                        column: x => x.companyid,
                        principalTable: "companys",
                        principalColumn: "comid");
                });

            migrationBuilder.CreateIndex(
                name: "ix_holidays_companyid",
                table: "holidays",
                column: "companyid");

            migrationBuilder.CreateIndex(
                name: "ix_leaves_companyid",
                table: "leaves",
                column: "companyid");

            migrationBuilder.CreateIndex(
                name: "ix_shifts_companyid",
                table: "shifts",
                column: "companyid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "holidays");

            migrationBuilder.DropTable(
                name: "leaves");

            migrationBuilder.DropTable(
                name: "shifts");
        }
    }
}
