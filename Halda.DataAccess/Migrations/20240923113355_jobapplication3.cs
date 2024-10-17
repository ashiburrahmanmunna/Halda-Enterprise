using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Halda.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class jobapplication3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_applicants_jobapplications_jobapplicationid",
                table: "applicants");

            migrationBuilder.DropIndex(
                name: "ix_applicants_jobapplicationid",
                table: "applicants");

            migrationBuilder.DropColumn(
                name: "jobapplicationid",
                table: "applicants");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "jobapplicationid",
                table: "applicants",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_applicants_jobapplicationid",
                table: "applicants",
                column: "jobapplicationid");

            migrationBuilder.AddForeignKey(
                name: "fk_applicants_jobapplications_jobapplicationid",
                table: "applicants",
                column: "jobapplicationid",
                principalTable: "jobapplications",
                principalColumn: "id");
        }
    }
}
