using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Halda.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class empleaveadjustaddleave2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_empleaveadjusts_leaves_leavedataid",
                table: "empleaveadjusts");

            migrationBuilder.RenameColumn(
                name: "leavedataid",
                table: "empleaveadjusts",
                newName: "leaveid");

            migrationBuilder.RenameIndex(
                name: "ix_empleaveadjusts_leavedataid",
                table: "empleaveadjusts",
                newName: "ix_empleaveadjusts_leaveid");

            migrationBuilder.AddForeignKey(
                name: "fk_empleaveadjusts_leaves_leaveid",
                table: "empleaveadjusts",
                column: "leaveid",
                principalTable: "leaves",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_empleaveadjusts_leaves_leaveid",
                table: "empleaveadjusts");

            migrationBuilder.RenameColumn(
                name: "leaveid",
                table: "empleaveadjusts",
                newName: "leavedataid");

            migrationBuilder.RenameIndex(
                name: "ix_empleaveadjusts_leaveid",
                table: "empleaveadjusts",
                newName: "ix_empleaveadjusts_leavedataid");

            migrationBuilder.AddForeignKey(
                name: "fk_empleaveadjusts_leaves_leavedataid",
                table: "empleaveadjusts",
                column: "leavedataid",
                principalTable: "leaves",
                principalColumn: "id");
        }
    }
}
