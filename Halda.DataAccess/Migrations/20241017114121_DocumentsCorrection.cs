using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Halda.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class DocumentsCorrection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "declaration",
                table: "employeedocuments",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "documentdate",
                table: "employeedocuments",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "declaration",
                table: "employeedocuments");

            migrationBuilder.DropColumn(
                name: "documentdate",
                table: "employeedocuments");
        }
    }
}
