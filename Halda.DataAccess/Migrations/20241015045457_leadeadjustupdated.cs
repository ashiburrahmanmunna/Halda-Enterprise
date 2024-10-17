using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Halda.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class leadeadjustupdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_empleaveadjusts_leaves_leaveid",
                table: "empleaveadjusts");

            migrationBuilder.DropIndex(
                name: "ix_empleaveadjusts_leaveid",
                table: "empleaveadjusts");

            migrationBuilder.DropColumn(
                name: "leaveid",
                table: "empleaveadjusts");

            migrationBuilder.CreateIndex(
                name: "ix_empleaveadjusts_adjustleaveid",
                table: "empleaveadjusts",
                column: "adjustleaveid");

            migrationBuilder.AddForeignKey(
                name: "fk_empleaveadjusts_leaves_adjustleaveid",
                table: "empleaveadjusts",
                column: "adjustleaveid",
                principalTable: "leaves",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_empleaveadjusts_leaves_adjustleaveid",
                table: "empleaveadjusts");

            migrationBuilder.DropIndex(
                name: "ix_empleaveadjusts_adjustleaveid",
                table: "empleaveadjusts");

            migrationBuilder.AddColumn<string>(
                name: "leaveid",
                table: "empleaveadjusts",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_empleaveadjusts_leaveid",
                table: "empleaveadjusts",
                column: "leaveid");

            migrationBuilder.AddForeignKey(
                name: "fk_empleaveadjusts_leaves_leaveid",
                table: "empleaveadjusts",
                column: "leaveid",
                principalTable: "leaves",
                principalColumn: "id");
        }
    }
}
