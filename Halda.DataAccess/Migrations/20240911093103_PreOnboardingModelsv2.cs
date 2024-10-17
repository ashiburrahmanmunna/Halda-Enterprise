using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Halda.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class PreOnboardingModelsv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_applicantsassignments_assignments_assignmentid",
                table: "applicantsassignments");

            migrationBuilder.DropPrimaryKey(
                name: "pk_milestones",
                table: "milestones");

            migrationBuilder.DropPrimaryKey(
                name: "pk_assignments",
                table: "assignments");

            migrationBuilder.DropColumn(
                name: "isdelete1",
                table: "jobapplications");

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "milestones",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "assignments",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "pk_milestones",
                table: "milestones",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_assignments",
                table: "assignments",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_applicantsassignments_assignments_assignmentid",
                table: "applicantsassignments",
                column: "assignmentid",
                principalTable: "assignments",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_applicantsassignments_assignments_assignmentid",
                table: "applicantsassignments");

            migrationBuilder.DropPrimaryKey(
                name: "pk_milestones",
                table: "milestones");

            migrationBuilder.DropPrimaryKey(
                name: "pk_assignments",
                table: "assignments");

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "milestones",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<bool>(
                name: "isdelete1",
                table: "jobapplications",
                type: "boolean",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "assignments",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "pk_milestones",
                table: "milestones",
                column: "name");

            migrationBuilder.AddPrimaryKey(
                name: "pk_assignments",
                table: "assignments",
                column: "title");

            migrationBuilder.AddForeignKey(
                name: "fk_applicantsassignments_assignments_assignmentid",
                table: "applicantsassignments",
                column: "assignmentid",
                principalTable: "assignments",
                principalColumn: "title",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
