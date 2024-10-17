using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Halda.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class jobapplication : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "applicantdocs");

            migrationBuilder.DropColumn(
                name: "currentsalary",
                table: "applicants");

            migrationBuilder.DropColumn(
                name: "expsalary",
                table: "applicants");

            migrationBuilder.RenameColumn(
                name: "university",
                table: "applicants",
                newName: "secmno");

            migrationBuilder.RenameColumn(
                name: "source",
                table: "applicants",
                newName: "religion");

            migrationBuilder.RenameColumn(
                name: "skills",
                table: "applicants",
                newName: "primarymno");

            migrationBuilder.RenameColumn(
                name: "prevorcurrcompany",
                table: "applicants",
                newName: "primaryemail");

            migrationBuilder.RenameColumn(
                name: "position",
                table: "applicants",
                newName: "passportnumberwithoutunderscores");

            migrationBuilder.RenameColumn(
                name: "phonenumber",
                table: "applicants",
                newName: "passportnumber");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "applicants",
                newName: "nid");

            migrationBuilder.RenameColumn(
                name: "location",
                table: "applicants",
                newName: "nationality");

            migrationBuilder.RenameColumn(
                name: "linkedin",
                table: "applicants",
                newName: "mothername");

            migrationBuilder.RenameColumn(
                name: "exp",
                table: "applicants",
                newName: "maritalstatus");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "applicants",
                newName: "lastname");

            migrationBuilder.RenameColumn(
                name: "coverletter",
                table: "applicants",
                newName: "firstname");

            migrationBuilder.AddColumn<DateTime>(
                name: "dob",
                table: "applicants",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "emergencycontact",
                table: "applicants",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "fathername",
                table: "applicants",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "gender",
                table: "applicants",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "passportissuedate",
                table: "applicants",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "dob",
                table: "applicants");

            migrationBuilder.DropColumn(
                name: "emergencycontact",
                table: "applicants");

            migrationBuilder.DropColumn(
                name: "fathername",
                table: "applicants");

            migrationBuilder.DropColumn(
                name: "gender",
                table: "applicants");

            migrationBuilder.DropColumn(
                name: "passportissuedate",
                table: "applicants");

            migrationBuilder.RenameColumn(
                name: "secmno",
                table: "applicants",
                newName: "university");

            migrationBuilder.RenameColumn(
                name: "religion",
                table: "applicants",
                newName: "source");

            migrationBuilder.RenameColumn(
                name: "primarymno",
                table: "applicants",
                newName: "skills");

            migrationBuilder.RenameColumn(
                name: "primaryemail",
                table: "applicants",
                newName: "prevorcurrcompany");

            migrationBuilder.RenameColumn(
                name: "passportnumberwithoutunderscores",
                table: "applicants",
                newName: "position");

            migrationBuilder.RenameColumn(
                name: "passportnumber",
                table: "applicants",
                newName: "phonenumber");

            migrationBuilder.RenameColumn(
                name: "nid",
                table: "applicants",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "nationality",
                table: "applicants",
                newName: "location");

            migrationBuilder.RenameColumn(
                name: "mothername",
                table: "applicants",
                newName: "linkedin");

            migrationBuilder.RenameColumn(
                name: "maritalstatus",
                table: "applicants",
                newName: "exp");

            migrationBuilder.RenameColumn(
                name: "lastname",
                table: "applicants",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "firstname",
                table: "applicants",
                newName: "coverletter");

            migrationBuilder.AddColumn<decimal>(
                name: "currentsalary",
                table: "applicants",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "expsalary",
                table: "applicants",
                type: "numeric",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "applicantdocs",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    applicantid = table.Column<string>(type: "text", nullable: false),
                    dateadded = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    dateupdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    filepath = table.Column<string>(type: "text", nullable: true),
                    isdelete = table.Column<bool>(type: "boolean", nullable: true),
                    type = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_applicantdocs", x => x.id);
                    table.ForeignKey(
                        name: "fk_applicantdocs_applicants_applicantid",
                        column: x => x.applicantid,
                        principalTable: "applicants",
                        principalColumn: "applicantid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_applicantdocs_applicantid",
                table: "applicantdocs",
                column: "applicantid");
        }
    }
}
