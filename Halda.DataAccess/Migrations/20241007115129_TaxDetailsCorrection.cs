using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Halda.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class TaxDetailsCorrection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_employeetaxs_employees_employeeid",
                table: "employeetaxs");

            migrationBuilder.DropColumn(
                name: "taxcertificatereceived",
                table: "employeetaxs");

            migrationBuilder.RenameColumn(
                name: "submitdate",
                table: "employeetaxs",
                newName: "returnsubmitdate");

            migrationBuilder.RenameColumn(
                name: "acknowledgmentslip",
                table: "employeetaxs",
                newName: "acknowledgmentsliprecdate");

            migrationBuilder.AlterColumn<string>(
                name: "taxyear",
                table: "employeetaxs",
                type: "text",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "taxextension",
                table: "employeetaxs",
                type: "boolean",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<bool>(
                name: "returnsubmit",
                table: "employeetaxs",
                type: "boolean",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "employeetaxs",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "employeeid",
                table: "employeetaxs",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<bool>(
                name: "taxcertificatereceive",
                table: "employeetaxs",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "fk_employeetaxs_employees_employeeid",
                table: "employeetaxs",
                column: "employeeid",
                principalTable: "employees",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_employeetaxs_employees_employeeid",
                table: "employeetaxs");

            migrationBuilder.DropColumn(
                name: "taxcertificatereceive",
                table: "employeetaxs");

            migrationBuilder.RenameColumn(
                name: "returnsubmitdate",
                table: "employeetaxs",
                newName: "submitdate");

            migrationBuilder.RenameColumn(
                name: "acknowledgmentsliprecdate",
                table: "employeetaxs",
                newName: "acknowledgmentslip");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "taxyear",
                table: "employeetaxs",
                type: "date",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "taxextension",
                table: "employeetaxs",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "returnsubmit",
                table: "employeetaxs",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "employeetaxs",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "employeeid",
                table: "employeetaxs",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "taxcertificatereceived",
                table: "employeetaxs",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "fk_employeetaxs_employees_employeeid",
                table: "employeetaxs",
                column: "employeeid",
                principalTable: "employees",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
