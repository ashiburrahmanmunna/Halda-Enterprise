using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Halda.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class empleaveadjustaddleave : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "leavedataid",
                table: "empleaveadjusts",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_empleaveadjusts_leavedataid",
                table: "empleaveadjusts",
                column: "leavedataid");

            migrationBuilder.AddForeignKey(
                name: "fk_empleaveadjusts_leaves_leavedataid",
                table: "empleaveadjusts",
                column: "leavedataid",
                principalTable: "leaves",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_empleaveadjusts_leaves_leavedataid",
                table: "empleaveadjusts");

            migrationBuilder.DropIndex(
                name: "ix_empleaveadjusts_leavedataid",
                table: "empleaveadjusts");

            migrationBuilder.DropColumn(
                name: "leavedataid",
                table: "empleaveadjusts");
        }
    }
}
