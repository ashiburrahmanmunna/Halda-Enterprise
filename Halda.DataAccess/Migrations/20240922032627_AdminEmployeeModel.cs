using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Halda.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AdminEmployeeModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "employeecode",
                table: "employees",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "employeetype",
                table: "employees",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "fingerid",
                table: "employees",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "joiningdate",
                table: "employees",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "oldfingerid",
                table: "employees",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "employeecode",
                table: "employees");

            migrationBuilder.DropColumn(
                name: "employeetype",
                table: "employees");

            migrationBuilder.DropColumn(
                name: "fingerid",
                table: "employees");

            migrationBuilder.DropColumn(
                name: "joiningdate",
                table: "employees");

            migrationBuilder.DropColumn(
                name: "oldfingerid",
                table: "employees");
        }
    }
}
