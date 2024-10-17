using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Halda.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class applicant_assignment2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "companyid",
                table: "applicantsassignments",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "jobpostid",
                table: "applicantsassignments",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_applicantsassignments_companyid",
                table: "applicantsassignments",
                column: "companyid");

            migrationBuilder.CreateIndex(
                name: "ix_applicantsassignments_jobpostid",
                table: "applicantsassignments",
                column: "jobpostid");

            migrationBuilder.AddForeignKey(
                name: "fk_applicantsassignments_companys_companyid",
                table: "applicantsassignments",
                column: "companyid",
                principalTable: "companys",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_applicantsassignments_jobposts_jobpostid",
                table: "applicantsassignments",
                column: "jobpostid",
                principalTable: "jobposts",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_applicantsassignments_companys_companyid",
                table: "applicantsassignments");

            migrationBuilder.DropForeignKey(
                name: "fk_applicantsassignments_jobposts_jobpostid",
                table: "applicantsassignments");

            migrationBuilder.DropIndex(
                name: "ix_applicantsassignments_companyid",
                table: "applicantsassignments");

            migrationBuilder.DropIndex(
                name: "ix_applicantsassignments_jobpostid",
                table: "applicantsassignments");

            migrationBuilder.DropColumn(
                name: "companyid",
                table: "applicantsassignments");

            migrationBuilder.DropColumn(
                name: "jobpostid",
                table: "applicantsassignments");
        }
    }
}
