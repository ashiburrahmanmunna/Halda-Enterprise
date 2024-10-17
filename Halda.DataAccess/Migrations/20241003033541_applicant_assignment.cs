using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Halda.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class applicant_assignment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_applicantsassignments_applicants_applicantid",
                table: "applicantsassignments");

            migrationBuilder.DropForeignKey(
                name: "fk_applicantsassignments_assignments_assignmentid",
                table: "applicantsassignments");

            migrationBuilder.AlterColumn<string>(
                name: "assignmentid",
                table: "applicantsassignments",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "applicantid",
                table: "applicantsassignments",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<DateTime>(
                name: "dateadded",
                table: "applicantsassignments",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "dateupdated",
                table: "applicantsassignments",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isdelete",
                table: "applicantsassignments",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "serial",
                table: "applicantsassignments",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "type",
                table: "applicantsassignments",
                type: "text",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "fk_applicantsassignments_applicants_applicantid",
                table: "applicantsassignments",
                column: "applicantid",
                principalTable: "applicants",
                principalColumn: "applicantid");

            migrationBuilder.AddForeignKey(
                name: "fk_applicantsassignments_assignments_assignmentid",
                table: "applicantsassignments",
                column: "assignmentid",
                principalTable: "assignments",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_applicantsassignments_applicants_applicantid",
                table: "applicantsassignments");

            migrationBuilder.DropForeignKey(
                name: "fk_applicantsassignments_assignments_assignmentid",
                table: "applicantsassignments");

            migrationBuilder.DropColumn(
                name: "dateadded",
                table: "applicantsassignments");

            migrationBuilder.DropColumn(
                name: "dateupdated",
                table: "applicantsassignments");

            migrationBuilder.DropColumn(
                name: "isdelete",
                table: "applicantsassignments");

            migrationBuilder.DropColumn(
                name: "serial",
                table: "applicantsassignments");

            migrationBuilder.DropColumn(
                name: "type",
                table: "applicantsassignments");

            migrationBuilder.AlterColumn<string>(
                name: "assignmentid",
                table: "applicantsassignments",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "applicantid",
                table: "applicantsassignments",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "fk_applicantsassignments_applicants_applicantid",
                table: "applicantsassignments",
                column: "applicantid",
                principalTable: "applicants",
                principalColumn: "applicantid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_applicantsassignments_assignments_assignmentid",
                table: "applicantsassignments",
                column: "assignmentid",
                principalTable: "assignments",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
