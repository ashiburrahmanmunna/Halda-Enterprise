using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Halda.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class JobDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_jobmilestones_jobposts_jobpostid",
                table: "jobmilestones");

            migrationBuilder.AddForeignKey(
                name: "fk_jobmilestones_jobposts_jobpostid",
                table: "jobmilestones",
                column: "jobpostid",
                principalTable: "jobposts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_jobmilestones_jobposts_jobpostid",
                table: "jobmilestones");

            migrationBuilder.AddForeignKey(
                name: "fk_jobmilestones_jobposts_jobpostid",
                table: "jobmilestones",
                column: "jobpostid",
                principalTable: "jobposts",
                principalColumn: "id");
        }
    }
}
