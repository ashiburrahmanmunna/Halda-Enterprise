using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Halda.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class assignment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_assignments_applicants_applicantid",
                table: "assignments");

            migrationBuilder.DropForeignKey(
                name: "fk_assignments_jobposts_jobpostid",
                table: "assignments");

            migrationBuilder.DropIndex(
                name: "ix_assignments_applicantid",
                table: "assignments");

            migrationBuilder.DropIndex(
                name: "ix_assignments_jobpostid",
                table: "assignments");

            migrationBuilder.DropColumn(
                name: "applicantid",
                table: "assignments");

            migrationBuilder.RenameColumn(
                name: "jobpostid",
                table: "assignments",
                newName: "files");

            migrationBuilder.AlterColumn<DateTime>(
                name: "duedate",
                table: "assignments",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "files",
                table: "assignments",
                newName: "jobpostid");

            migrationBuilder.AlterColumn<DateTime>(
                name: "duedate",
                table: "assignments",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "applicantid",
                table: "assignments",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_assignments_applicantid",
                table: "assignments",
                column: "applicantid");

            migrationBuilder.CreateIndex(
                name: "ix_assignments_jobpostid",
                table: "assignments",
                column: "jobpostid");

            migrationBuilder.AddForeignKey(
                name: "fk_assignments_applicants_applicantid",
                table: "assignments",
                column: "applicantid",
                principalTable: "applicants",
                principalColumn: "applicantid");

            migrationBuilder.AddForeignKey(
                name: "fk_assignments_jobposts_jobpostid",
                table: "assignments",
                column: "jobpostid",
                principalTable: "jobposts",
                principalColumn: "id");
        }
    }
}
