using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HCMS.Data.Migrations
{
    public partial class ChangeSalaryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "deleted_on",
                table: "Salary",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "effective_to",
                table: "Salary",
                nullable: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                table: "Salary",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "periodicity",
                table: "Salary",
                nullable: false,
                defaultValue: 1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "deleted_on",
                table: "Salary");

            migrationBuilder.DropColumn(
                name: "effective_to",
                table: "Salary");

            migrationBuilder.DropColumn(
                name: "is_deleted",
                table: "Salary");

            migrationBuilder.DropColumn(
                name: "periodicity",
                table: "Salary");
        }
    }
}
