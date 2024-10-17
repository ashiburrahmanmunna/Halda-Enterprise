using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Halda.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class PreOnboardingModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_applicantapplicationstatus_applicants_applicant_id",
                table: "applicantapplicationstatus");

            migrationBuilder.DropForeignKey(
                name: "fk_applicantapplicationstatus_jobmilestones_milestone_id",
                table: "applicantapplicationstatus");

            migrationBuilder.DropForeignKey(
                name: "fk_applicantapplicationstatus_jobposts_jobpost_id",
                table: "applicantapplicationstatus");

            migrationBuilder.DropForeignKey(
                name: "fk_applicantsassignments_assignments_assignmentid",
                table: "applicantsassignments");

            migrationBuilder.DropForeignKey(
                name: "fk_jobmilestones_jobposts_jobpostsid",
                table: "jobmilestones");

            migrationBuilder.DropForeignKey(
                name: "fk_milestones_jobdescriptiontemplates_jobdescription_id",
                table: "milestones");

            migrationBuilder.DropPrimaryKey(
                name: "pk_milestones",
                table: "milestones");

            migrationBuilder.DropPrimaryKey(
                name: "pk_assignments",
                table: "assignments");

            migrationBuilder.DropColumn(
                name: "jobpost_id",
                table: "jobmilestones");

            migrationBuilder.DropColumn(
                name: "designation_id",
                table: "jobdescriptiontemplates");

            migrationBuilder.RenameColumn(
                name: "jobdescription_id",
                table: "milestones",
                newName: "jobdescriptionid");

            migrationBuilder.RenameIndex(
                name: "ix_milestones_jobdescription_id",
                table: "milestones",
                newName: "ix_milestones_jobdescriptionid");

            migrationBuilder.RenameColumn(
                name: "desination_id",
                table: "jobposts",
                newName: "desinationid");

            migrationBuilder.RenameColumn(
                name: "department_id",
                table: "jobposts",
                newName: "departmentid");

            migrationBuilder.RenameColumn(
                name: "jobpostsid",
                table: "jobmilestones",
                newName: "jobpostid");

            migrationBuilder.RenameIndex(
                name: "ix_jobmilestones_jobpostsid",
                table: "jobmilestones",
                newName: "ix_jobmilestones_jobpostid");

            migrationBuilder.RenameColumn(
                name: "milestone_id",
                table: "applicantapplicationstatus",
                newName: "milestoneid");

            migrationBuilder.RenameColumn(
                name: "jobpost_id",
                table: "applicantapplicationstatus",
                newName: "jobpostid");

            migrationBuilder.RenameColumn(
                name: "applicant_id",
                table: "applicantapplicationstatus",
                newName: "applicantid");

            migrationBuilder.RenameIndex(
                name: "ix_applicantapplicationstatus_milestone_id",
                table: "applicantapplicationstatus",
                newName: "ix_applicantapplicationstatus_milestoneid");

            migrationBuilder.RenameIndex(
                name: "ix_applicantapplicationstatus_jobpost_id",
                table: "applicantapplicationstatus",
                newName: "ix_applicantapplicationstatus_jobpostid");

            migrationBuilder.RenameIndex(
                name: "ix_applicantapplicationstatus_applicant_id",
                table: "applicantapplicationstatus",
                newName: "ix_applicantapplicationstatus_applicantid");

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "milestones",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<DateTime>(
                name: "dateadded",
                table: "jobapplications",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "dateupdated",
                table: "jobapplications",
                type: "timestamp with time zone",
                nullable: true);

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
                name: "fk_applicantapplicationstatus_applicants_applicantid",
                table: "applicantapplicationstatus",
                column: "applicantid",
                principalTable: "applicants",
                principalColumn: "applicantid");

            migrationBuilder.AddForeignKey(
                name: "fk_applicantapplicationstatus_jobmilestones_milestoneid",
                table: "applicantapplicationstatus",
                column: "milestoneid",
                principalTable: "jobmilestones",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_applicantapplicationstatus_jobposts_jobpostid",
                table: "applicantapplicationstatus",
                column: "jobpostid",
                principalTable: "jobposts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_applicantsassignments_assignments_assignmentid",
                table: "applicantsassignments",
                column: "assignmentid",
                principalTable: "assignments",
                principalColumn: "title",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_jobmilestones_jobposts_jobpostid",
                table: "jobmilestones",
                column: "jobpostid",
                principalTable: "jobposts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_milestones_jobdescriptiontemplates_jobdescriptionid",
                table: "milestones",
                column: "jobdescriptionid",
                principalTable: "jobdescriptiontemplates",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_applicantapplicationstatus_applicants_applicantid",
                table: "applicantapplicationstatus");

            migrationBuilder.DropForeignKey(
                name: "fk_applicantapplicationstatus_jobmilestones_milestoneid",
                table: "applicantapplicationstatus");

            migrationBuilder.DropForeignKey(
                name: "fk_applicantapplicationstatus_jobposts_jobpostid",
                table: "applicantapplicationstatus");

            migrationBuilder.DropForeignKey(
                name: "fk_applicantsassignments_assignments_assignmentid",
                table: "applicantsassignments");

            migrationBuilder.DropForeignKey(
                name: "fk_jobmilestones_jobposts_jobpostid",
                table: "jobmilestones");

            migrationBuilder.DropForeignKey(
                name: "fk_milestones_jobdescriptiontemplates_jobdescriptionid",
                table: "milestones");

            migrationBuilder.DropPrimaryKey(
                name: "pk_milestones",
                table: "milestones");

            migrationBuilder.DropPrimaryKey(
                name: "pk_assignments",
                table: "assignments");

            migrationBuilder.DropColumn(
                name: "dateadded",
                table: "jobapplications");

            migrationBuilder.DropColumn(
                name: "dateupdated",
                table: "jobapplications");

            migrationBuilder.DropColumn(
                name: "isdelete1",
                table: "jobapplications");

            migrationBuilder.RenameColumn(
                name: "jobdescriptionid",
                table: "milestones",
                newName: "jobdescription_id");

            migrationBuilder.RenameIndex(
                name: "ix_milestones_jobdescriptionid",
                table: "milestones",
                newName: "ix_milestones_jobdescription_id");

            migrationBuilder.RenameColumn(
                name: "desinationid",
                table: "jobposts",
                newName: "desination_id");

            migrationBuilder.RenameColumn(
                name: "departmentid",
                table: "jobposts",
                newName: "department_id");

            migrationBuilder.RenameColumn(
                name: "jobpostid",
                table: "jobmilestones",
                newName: "jobpostsid");

            migrationBuilder.RenameIndex(
                name: "ix_jobmilestones_jobpostid",
                table: "jobmilestones",
                newName: "ix_jobmilestones_jobpostsid");

            migrationBuilder.RenameColumn(
                name: "milestoneid",
                table: "applicantapplicationstatus",
                newName: "milestone_id");

            migrationBuilder.RenameColumn(
                name: "jobpostid",
                table: "applicantapplicationstatus",
                newName: "jobpost_id");

            migrationBuilder.RenameColumn(
                name: "applicantid",
                table: "applicantapplicationstatus",
                newName: "applicant_id");

            migrationBuilder.RenameIndex(
                name: "ix_applicantapplicationstatus_milestoneid",
                table: "applicantapplicationstatus",
                newName: "ix_applicantapplicationstatus_milestone_id");

            migrationBuilder.RenameIndex(
                name: "ix_applicantapplicationstatus_jobpostid",
                table: "applicantapplicationstatus",
                newName: "ix_applicantapplicationstatus_jobpost_id");

            migrationBuilder.RenameIndex(
                name: "ix_applicantapplicationstatus_applicantid",
                table: "applicantapplicationstatus",
                newName: "ix_applicantapplicationstatus_applicant_id");

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "milestones",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "jobpost_id",
                table: "jobmilestones",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "designation_id",
                table: "jobdescriptiontemplates",
                type: "text",
                nullable: true);

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
                name: "fk_applicantapplicationstatus_applicants_applicant_id",
                table: "applicantapplicationstatus",
                column: "applicant_id",
                principalTable: "applicants",
                principalColumn: "applicantid");

            migrationBuilder.AddForeignKey(
                name: "fk_applicantapplicationstatus_jobmilestones_milestone_id",
                table: "applicantapplicationstatus",
                column: "milestone_id",
                principalTable: "jobmilestones",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_applicantapplicationstatus_jobposts_jobpost_id",
                table: "applicantapplicationstatus",
                column: "jobpost_id",
                principalTable: "jobposts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_applicantsassignments_assignments_assignmentid",
                table: "applicantsassignments",
                column: "assignmentid",
                principalTable: "assignments",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_jobmilestones_jobposts_jobpostsid",
                table: "jobmilestones",
                column: "jobpostsid",
                principalTable: "jobposts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_milestones_jobdescriptiontemplates_jobdescription_id",
                table: "milestones",
                column: "jobdescription_id",
                principalTable: "jobdescriptiontemplates",
                principalColumn: "id");
        }
    }
}
