using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Halda.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class EmpContractPeriod : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_assignments_companys_companyid",
                table: "assignments");

            migrationBuilder.DropForeignKey(
                name: "fk_departments_companys_companyid",
                table: "departments");

            migrationBuilder.DropForeignKey(
                name: "fk_designations_companys_companyid",
                table: "designations");

            migrationBuilder.DropForeignKey(
                name: "fk_empedus_companys_companyid",
                table: "empedus");

            migrationBuilder.DropForeignKey(
                name: "fk_employeeaddresses_companys_companyid",
                table: "employeeaddresses");

            migrationBuilder.DropForeignKey(
                name: "fk_employeebanks_companys_companyid",
                table: "employeebanks");

            migrationBuilder.DropForeignKey(
                name: "fk_employeeemergencycontacts_companys_companyid",
                table: "employeeemergencycontacts");

            migrationBuilder.DropForeignKey(
                name: "fk_employeefamilynomineeinfos_companys_companyid",
                table: "employeefamilynomineeinfos");

            migrationBuilder.DropForeignKey(
                name: "fk_employees_companys_companyid",
                table: "employees");

            migrationBuilder.DropForeignKey(
                name: "fk_employeetaxs_companys_companyid",
                table: "employeetaxs");

            migrationBuilder.DropForeignKey(
                name: "fk_floors_companys_companyid",
                table: "floors");

            migrationBuilder.DropForeignKey(
                name: "fk_holidays_companys_companyid",
                table: "holidays");

            migrationBuilder.DropForeignKey(
                name: "fk_jobapplications_companys_companyid",
                table: "jobapplications");

            migrationBuilder.DropForeignKey(
                name: "fk_jobdescriptiontemplates_companys_companyid",
                table: "jobdescriptiontemplates");

            migrationBuilder.DropForeignKey(
                name: "fk_jobmilestones_companys_companyid",
                table: "jobmilestones");

            migrationBuilder.DropForeignKey(
                name: "fk_jobposts_companys_companyid",
                table: "jobposts");

            migrationBuilder.DropForeignKey(
                name: "fk_leaves_companys_companyid",
                table: "leaves");

            migrationBuilder.DropForeignKey(
                name: "fk_lines_companys_companyid",
                table: "lines");

            migrationBuilder.DropForeignKey(
                name: "fk_milestones_companys_companyid",
                table: "milestones");

            migrationBuilder.DropForeignKey(
                name: "fk_recruitmentvariables_companys_companyid",
                table: "recruitmentvariables");

            migrationBuilder.DropForeignKey(
                name: "fk_sections_companys_companyid",
                table: "sections");

            migrationBuilder.DropForeignKey(
                name: "fk_shifts_companys_companyid",
                table: "shifts");

            migrationBuilder.DropForeignKey(
                name: "fk_users_companys_companyid",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "pk_companys",
                table: "companys");

            migrationBuilder.DropColumn(
                name: "shiftid",
                table: "shifts");

            migrationBuilder.DropColumn(
                name: "comid",
                table: "companys");

            migrationBuilder.AddPrimaryKey(
                name: "pk_companys",
                table: "companys",
                column: "id");

            migrationBuilder.CreateTable(
                name: "empcontractperiod",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    contractperiod = table.Column<string>(type: "text", nullable: true),
                    status = table.Column<string>(type: "text", nullable: true),
                    contractstartdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    contractenddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    attachments = table.Column<string>(type: "text", nullable: true),
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
                    table.PrimaryKey("pk_empcontractperiod", x => x.id);
                    table.ForeignKey(
                        name: "fk_empcontractperiod_companys_companyid",
                        column: x => x.companyid,
                        principalTable: "companys",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_empcontractperiod_companyid",
                table: "empcontractperiod",
                column: "companyid");

            migrationBuilder.AddForeignKey(
                name: "fk_assignments_companys_companyid",
                table: "assignments",
                column: "companyid",
                principalTable: "companys",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_departments_companys_companyid",
                table: "departments",
                column: "companyid",
                principalTable: "companys",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_designations_companys_companyid",
                table: "designations",
                column: "companyid",
                principalTable: "companys",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_empedus_companys_companyid",
                table: "empedus",
                column: "companyid",
                principalTable: "companys",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_employeeaddresses_companys_companyid",
                table: "employeeaddresses",
                column: "companyid",
                principalTable: "companys",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_employeebanks_companys_companyid",
                table: "employeebanks",
                column: "companyid",
                principalTable: "companys",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_employeeemergencycontacts_companys_companyid",
                table: "employeeemergencycontacts",
                column: "companyid",
                principalTable: "companys",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_employeefamilynomineeinfos_companys_companyid",
                table: "employeefamilynomineeinfos",
                column: "companyid",
                principalTable: "companys",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_employees_companys_companyid",
                table: "employees",
                column: "companyid",
                principalTable: "companys",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_employeetaxs_companys_companyid",
                table: "employeetaxs",
                column: "companyid",
                principalTable: "companys",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_floors_companys_companyid",
                table: "floors",
                column: "companyid",
                principalTable: "companys",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_holidays_companys_companyid",
                table: "holidays",
                column: "companyid",
                principalTable: "companys",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_jobapplications_companys_companyid",
                table: "jobapplications",
                column: "companyid",
                principalTable: "companys",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_jobdescriptiontemplates_companys_companyid",
                table: "jobdescriptiontemplates",
                column: "companyid",
                principalTable: "companys",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_jobmilestones_companys_companyid",
                table: "jobmilestones",
                column: "companyid",
                principalTable: "companys",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_jobposts_companys_companyid",
                table: "jobposts",
                column: "companyid",
                principalTable: "companys",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_leaves_companys_companyid",
                table: "leaves",
                column: "companyid",
                principalTable: "companys",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_lines_companys_companyid",
                table: "lines",
                column: "companyid",
                principalTable: "companys",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_milestones_companys_companyid",
                table: "milestones",
                column: "companyid",
                principalTable: "companys",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_recruitmentvariables_companys_companyid",
                table: "recruitmentvariables",
                column: "companyid",
                principalTable: "companys",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_sections_companys_companyid",
                table: "sections",
                column: "companyid",
                principalTable: "companys",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_shifts_companys_companyid",
                table: "shifts",
                column: "companyid",
                principalTable: "companys",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_users_companys_companyid",
                table: "users",
                column: "companyid",
                principalTable: "companys",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_assignments_companys_companyid",
                table: "assignments");

            migrationBuilder.DropForeignKey(
                name: "fk_departments_companys_companyid",
                table: "departments");

            migrationBuilder.DropForeignKey(
                name: "fk_designations_companys_companyid",
                table: "designations");

            migrationBuilder.DropForeignKey(
                name: "fk_empedus_companys_companyid",
                table: "empedus");

            migrationBuilder.DropForeignKey(
                name: "fk_employeeaddresses_companys_companyid",
                table: "employeeaddresses");

            migrationBuilder.DropForeignKey(
                name: "fk_employeebanks_companys_companyid",
                table: "employeebanks");

            migrationBuilder.DropForeignKey(
                name: "fk_employeeemergencycontacts_companys_companyid",
                table: "employeeemergencycontacts");

            migrationBuilder.DropForeignKey(
                name: "fk_employeefamilynomineeinfos_companys_companyid",
                table: "employeefamilynomineeinfos");

            migrationBuilder.DropForeignKey(
                name: "fk_employees_companys_companyid",
                table: "employees");

            migrationBuilder.DropForeignKey(
                name: "fk_employeetaxs_companys_companyid",
                table: "employeetaxs");

            migrationBuilder.DropForeignKey(
                name: "fk_floors_companys_companyid",
                table: "floors");

            migrationBuilder.DropForeignKey(
                name: "fk_holidays_companys_companyid",
                table: "holidays");

            migrationBuilder.DropForeignKey(
                name: "fk_jobapplications_companys_companyid",
                table: "jobapplications");

            migrationBuilder.DropForeignKey(
                name: "fk_jobdescriptiontemplates_companys_companyid",
                table: "jobdescriptiontemplates");

            migrationBuilder.DropForeignKey(
                name: "fk_jobmilestones_companys_companyid",
                table: "jobmilestones");

            migrationBuilder.DropForeignKey(
                name: "fk_jobposts_companys_companyid",
                table: "jobposts");

            migrationBuilder.DropForeignKey(
                name: "fk_leaves_companys_companyid",
                table: "leaves");

            migrationBuilder.DropForeignKey(
                name: "fk_lines_companys_companyid",
                table: "lines");

            migrationBuilder.DropForeignKey(
                name: "fk_milestones_companys_companyid",
                table: "milestones");

            migrationBuilder.DropForeignKey(
                name: "fk_recruitmentvariables_companys_companyid",
                table: "recruitmentvariables");

            migrationBuilder.DropForeignKey(
                name: "fk_sections_companys_companyid",
                table: "sections");

            migrationBuilder.DropForeignKey(
                name: "fk_shifts_companys_companyid",
                table: "shifts");

            migrationBuilder.DropForeignKey(
                name: "fk_users_companys_companyid",
                table: "users");

            migrationBuilder.DropTable(
                name: "empcontractperiod");

            migrationBuilder.DropPrimaryKey(
                name: "pk_companys",
                table: "companys");

            migrationBuilder.AddColumn<int>(
                name: "shiftid",
                table: "shifts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "comid",
                table: "companys",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "pk_companys",
                table: "companys",
                column: "comid");

            migrationBuilder.AddForeignKey(
                name: "fk_assignments_companys_companyid",
                table: "assignments",
                column: "companyid",
                principalTable: "companys",
                principalColumn: "comid");

            migrationBuilder.AddForeignKey(
                name: "fk_departments_companys_companyid",
                table: "departments",
                column: "companyid",
                principalTable: "companys",
                principalColumn: "comid");

            migrationBuilder.AddForeignKey(
                name: "fk_designations_companys_companyid",
                table: "designations",
                column: "companyid",
                principalTable: "companys",
                principalColumn: "comid");

            migrationBuilder.AddForeignKey(
                name: "fk_empedus_companys_companyid",
                table: "empedus",
                column: "companyid",
                principalTable: "companys",
                principalColumn: "comid");

            migrationBuilder.AddForeignKey(
                name: "fk_employeeaddresses_companys_companyid",
                table: "employeeaddresses",
                column: "companyid",
                principalTable: "companys",
                principalColumn: "comid");

            migrationBuilder.AddForeignKey(
                name: "fk_employeebanks_companys_companyid",
                table: "employeebanks",
                column: "companyid",
                principalTable: "companys",
                principalColumn: "comid");

            migrationBuilder.AddForeignKey(
                name: "fk_employeeemergencycontacts_companys_companyid",
                table: "employeeemergencycontacts",
                column: "companyid",
                principalTable: "companys",
                principalColumn: "comid");

            migrationBuilder.AddForeignKey(
                name: "fk_employeefamilynomineeinfos_companys_companyid",
                table: "employeefamilynomineeinfos",
                column: "companyid",
                principalTable: "companys",
                principalColumn: "comid");

            migrationBuilder.AddForeignKey(
                name: "fk_employees_companys_companyid",
                table: "employees",
                column: "companyid",
                principalTable: "companys",
                principalColumn: "comid");

            migrationBuilder.AddForeignKey(
                name: "fk_employeetaxs_companys_companyid",
                table: "employeetaxs",
                column: "companyid",
                principalTable: "companys",
                principalColumn: "comid");

            migrationBuilder.AddForeignKey(
                name: "fk_floors_companys_companyid",
                table: "floors",
                column: "companyid",
                principalTable: "companys",
                principalColumn: "comid");

            migrationBuilder.AddForeignKey(
                name: "fk_holidays_companys_companyid",
                table: "holidays",
                column: "companyid",
                principalTable: "companys",
                principalColumn: "comid");

            migrationBuilder.AddForeignKey(
                name: "fk_jobapplications_companys_companyid",
                table: "jobapplications",
                column: "companyid",
                principalTable: "companys",
                principalColumn: "comid");

            migrationBuilder.AddForeignKey(
                name: "fk_jobdescriptiontemplates_companys_companyid",
                table: "jobdescriptiontemplates",
                column: "companyid",
                principalTable: "companys",
                principalColumn: "comid");

            migrationBuilder.AddForeignKey(
                name: "fk_jobmilestones_companys_companyid",
                table: "jobmilestones",
                column: "companyid",
                principalTable: "companys",
                principalColumn: "comid");

            migrationBuilder.AddForeignKey(
                name: "fk_jobposts_companys_companyid",
                table: "jobposts",
                column: "companyid",
                principalTable: "companys",
                principalColumn: "comid");

            migrationBuilder.AddForeignKey(
                name: "fk_leaves_companys_companyid",
                table: "leaves",
                column: "companyid",
                principalTable: "companys",
                principalColumn: "comid");

            migrationBuilder.AddForeignKey(
                name: "fk_lines_companys_companyid",
                table: "lines",
                column: "companyid",
                principalTable: "companys",
                principalColumn: "comid");

            migrationBuilder.AddForeignKey(
                name: "fk_milestones_companys_companyid",
                table: "milestones",
                column: "companyid",
                principalTable: "companys",
                principalColumn: "comid");

            migrationBuilder.AddForeignKey(
                name: "fk_recruitmentvariables_companys_companyid",
                table: "recruitmentvariables",
                column: "companyid",
                principalTable: "companys",
                principalColumn: "comid");

            migrationBuilder.AddForeignKey(
                name: "fk_sections_companys_companyid",
                table: "sections",
                column: "companyid",
                principalTable: "companys",
                principalColumn: "comid");

            migrationBuilder.AddForeignKey(
                name: "fk_shifts_companys_companyid",
                table: "shifts",
                column: "companyid",
                principalTable: "companys",
                principalColumn: "comid");

            migrationBuilder.AddForeignKey(
                name: "fk_users_companys_companyid",
                table: "users",
                column: "companyid",
                principalTable: "companys",
                principalColumn: "comid");
        }
    }
}
