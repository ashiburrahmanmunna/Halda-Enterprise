using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Halda.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AllModelUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_citys_states_stateid",
                table: "citys");

            migrationBuilder.DropPrimaryKey(
                name: "pk_states",
                table: "states");

            migrationBuilder.DropPrimaryKey(
                name: "pk_citys",
                table: "citys");

            migrationBuilder.DropColumn(
                name: "attbonus",
                table: "designations");

            migrationBuilder.DropColumn(
                name: "designameb",
                table: "designations");

            migrationBuilder.DropColumn(
                name: "dtinput",
                table: "designations");

            migrationBuilder.DropColumn(
                name: "gsmin",
                table: "designations");

            migrationBuilder.DropColumn(
                name: "holidaybonus",
                table: "designations");

            migrationBuilder.DropColumn(
                name: "nightallow",
                table: "designations");

            migrationBuilder.DropColumn(
                name: "proposedmanpower",
                table: "designations");

            migrationBuilder.DropColumn(
                name: "salaryrange",
                table: "designations");

            migrationBuilder.DropColumn(
                name: "slno",
                table: "designations");

            migrationBuilder.DropColumn(
                name: "ttlmanpower",
                table: "designations");

            migrationBuilder.DropColumn(
                name: "deptbangla",
                table: "departments");

            migrationBuilder.DropColumn(
                name: "deptclevelid",
                table: "departments");

            migrationBuilder.DropColumn(
                name: "depthodid",
                table: "departments");

            migrationBuilder.DropColumn(
                name: "dtinput",
                table: "departments");

            migrationBuilder.DropColumn(
                name: "isactualothide",
                table: "departments");

            migrationBuilder.DropColumn(
                name: "isactualsalaryhide",
                table: "departments");

            migrationBuilder.DropColumn(
                name: "isbuyerotdiffer",
                table: "departments");

            migrationBuilder.DropColumn(
                name: "isbuyerothide",
                table: "departments");

            migrationBuilder.DropColumn(
                name: "isbuyersalaryhide",
                table: "departments");

            migrationBuilder.DropColumn(
                name: "pcname",
                table: "departments");

            migrationBuilder.DropColumn(
                name: "slno",
                table: "departments");

            migrationBuilder.RenameColumn(
                name: "pcname",
                table: "designations",
                newName: "desigcode");

            migrationBuilder.AddColumn<string>(
                name: "id",
                table: "states",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "departmentid",
                table: "employees",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "designationid",
                table: "employees",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "floorid",
                table: "employees",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "lineid",
                table: "employees",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sectionid",
                table: "employees",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "localname",
                table: "designations",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "order",
                table: "designations",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "deptcode",
                table: "departments",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "localname",
                table: "departments",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "order",
                table: "departments",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "id",
                table: "countries",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "id",
                table: "companys",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "id",
                table: "citys",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "pk_states",
                table: "states",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_citys",
                table: "citys",
                column: "id");

            migrationBuilder.CreateTable(
                name: "floors",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    floorname = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    floorcode = table.Column<string>(type: "text", nullable: true),
                    localname = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    order = table.Column<int>(type: "integer", nullable: false),
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
                    table.PrimaryKey("pk_floors", x => x.id);
                    table.ForeignKey(
                        name: "fk_floors_companys_companyid",
                        column: x => x.companyid,
                        principalTable: "companys",
                        principalColumn: "comid");
                });

            migrationBuilder.CreateTable(
                name: "lines",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    linename = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    linecode = table.Column<string>(type: "text", nullable: true),
                    localname = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    order = table.Column<int>(type: "integer", nullable: false),
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
                    table.PrimaryKey("pk_lines", x => x.id);
                    table.ForeignKey(
                        name: "fk_lines_companys_companyid",
                        column: x => x.companyid,
                        principalTable: "companys",
                        principalColumn: "comid");
                });

            migrationBuilder.CreateTable(
                name: "sections",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    secname = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    seccode = table.Column<string>(type: "text", nullable: true),
                    localname = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    order = table.Column<int>(type: "integer", nullable: false),
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
                    table.PrimaryKey("pk_sections", x => x.id);
                    table.ForeignKey(
                        name: "fk_sections_companys_companyid",
                        column: x => x.companyid,
                        principalTable: "companys",
                        principalColumn: "comid");
                });

            migrationBuilder.CreateTable(
                name: "variabledata",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    type = table.Column<string>(type: "text", nullable: false),
                    value = table.Column<string>(type: "text", nullable: false),
                    localname = table.Column<string>(type: "text", nullable: true),
                    order = table.Column<int>(type: "integer", nullable: false),
                    isdelete = table.Column<bool>(type: "boolean", nullable: true),
                    dateadded = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    dateupdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_variabledata", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_employees_departmentid",
                table: "employees",
                column: "departmentid");

            migrationBuilder.CreateIndex(
                name: "ix_employees_designationid",
                table: "employees",
                column: "designationid");

            migrationBuilder.CreateIndex(
                name: "ix_employees_floorid",
                table: "employees",
                column: "floorid");

            migrationBuilder.CreateIndex(
                name: "ix_employees_lineid",
                table: "employees",
                column: "lineid");

            migrationBuilder.CreateIndex(
                name: "ix_employees_sectionid",
                table: "employees",
                column: "sectionid");

            migrationBuilder.CreateIndex(
                name: "ix_floors_companyid",
                table: "floors",
                column: "companyid");

            migrationBuilder.CreateIndex(
                name: "ix_lines_companyid",
                table: "lines",
                column: "companyid");

            migrationBuilder.CreateIndex(
                name: "ix_sections_companyid",
                table: "sections",
                column: "companyid");

            migrationBuilder.AddForeignKey(
                name: "fk_citys_states_stateid",
                table: "citys",
                column: "stateid",
                principalTable: "states",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_employees_departments_departmentid",
                table: "employees",
                column: "departmentid",
                principalTable: "departments",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_employees_designations_designationid",
                table: "employees",
                column: "designationid",
                principalTable: "designations",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_employees_floors_floorid",
                table: "employees",
                column: "floorid",
                principalTable: "floors",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_employees_lines_lineid",
                table: "employees",
                column: "lineid",
                principalTable: "lines",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_employees_sections_sectionid",
                table: "employees",
                column: "sectionid",
                principalTable: "sections",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_citys_states_stateid",
                table: "citys");

            migrationBuilder.DropForeignKey(
                name: "fk_employees_departments_departmentid",
                table: "employees");

            migrationBuilder.DropForeignKey(
                name: "fk_employees_designations_designationid",
                table: "employees");

            migrationBuilder.DropForeignKey(
                name: "fk_employees_floors_floorid",
                table: "employees");

            migrationBuilder.DropForeignKey(
                name: "fk_employees_lines_lineid",
                table: "employees");

            migrationBuilder.DropForeignKey(
                name: "fk_employees_sections_sectionid",
                table: "employees");

            migrationBuilder.DropTable(
                name: "floors");

            migrationBuilder.DropTable(
                name: "lines");

            migrationBuilder.DropTable(
                name: "sections");

            migrationBuilder.DropTable(
                name: "variabledata");

            migrationBuilder.DropPrimaryKey(
                name: "pk_states",
                table: "states");

            migrationBuilder.DropIndex(
                name: "ix_employees_departmentid",
                table: "employees");

            migrationBuilder.DropIndex(
                name: "ix_employees_designationid",
                table: "employees");

            migrationBuilder.DropIndex(
                name: "ix_employees_floorid",
                table: "employees");

            migrationBuilder.DropIndex(
                name: "ix_employees_lineid",
                table: "employees");

            migrationBuilder.DropIndex(
                name: "ix_employees_sectionid",
                table: "employees");

            migrationBuilder.DropPrimaryKey(
                name: "pk_citys",
                table: "citys");

            migrationBuilder.DropColumn(
                name: "id",
                table: "states");

            migrationBuilder.DropColumn(
                name: "departmentid",
                table: "employees");

            migrationBuilder.DropColumn(
                name: "designationid",
                table: "employees");

            migrationBuilder.DropColumn(
                name: "floorid",
                table: "employees");

            migrationBuilder.DropColumn(
                name: "lineid",
                table: "employees");

            migrationBuilder.DropColumn(
                name: "sectionid",
                table: "employees");

            migrationBuilder.DropColumn(
                name: "localname",
                table: "designations");

            migrationBuilder.DropColumn(
                name: "order",
                table: "designations");

            migrationBuilder.DropColumn(
                name: "localname",
                table: "departments");

            migrationBuilder.DropColumn(
                name: "order",
                table: "departments");

            migrationBuilder.DropColumn(
                name: "id",
                table: "countries");

            migrationBuilder.DropColumn(
                name: "id",
                table: "companys");

            migrationBuilder.DropColumn(
                name: "id",
                table: "citys");

            migrationBuilder.RenameColumn(
                name: "desigcode",
                table: "designations",
                newName: "pcname");

            migrationBuilder.AddColumn<decimal>(
                name: "attbonus",
                table: "designations",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "designameb",
                table: "designations",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "dtinput",
                table: "designations",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "gsmin",
                table: "designations",
                type: "numeric(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "holidaybonus",
                table: "designations",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "nightallow",
                table: "designations",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "proposedmanpower",
                table: "designations",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "salaryrange",
                table: "designations",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "slno",
                table: "designations",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ttlmanpower",
                table: "designations",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "deptcode",
                table: "departments",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "deptbangla",
                table: "departments",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "deptclevelid",
                table: "departments",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "depthodid",
                table: "departments",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "dtinput",
                table: "departments",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "isactualothide",
                table: "departments",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isactualsalaryhide",
                table: "departments",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isbuyerotdiffer",
                table: "departments",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isbuyerothide",
                table: "departments",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isbuyersalaryhide",
                table: "departments",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "pcname",
                table: "departments",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<short>(
                name: "slno",
                table: "departments",
                type: "smallint",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "pk_states",
                table: "states",
                column: "stateid");

            migrationBuilder.AddPrimaryKey(
                name: "pk_citys",
                table: "citys",
                column: "cityid");

            migrationBuilder.AddForeignKey(
                name: "fk_citys_states_stateid",
                table: "citys",
                column: "stateid",
                principalTable: "states",
                principalColumn: "stateid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
