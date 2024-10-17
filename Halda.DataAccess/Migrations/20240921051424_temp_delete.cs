using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Halda.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class temp_delete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_milestones_jobdescriptiontemplates_jobdescriptionid",
                table: "milestones");

            migrationBuilder.AddForeignKey(
                name: "fk_milestones_jobdescriptiontemplates_jobdescriptionid",
                table: "milestones",
                column: "jobdescriptionid",
                principalTable: "jobdescriptiontemplates",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_milestones_jobdescriptiontemplates_jobdescriptionid",
                table: "milestones");

            migrationBuilder.AddForeignKey(
                name: "fk_milestones_jobdescriptiontemplates_jobdescriptionid",
                table: "milestones",
                column: "jobdescriptionid",
                principalTable: "jobdescriptiontemplates",
                principalColumn: "id");
        }
    }
}
