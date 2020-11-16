using Microsoft.EntityFrameworkCore.Migrations;

namespace HCMS.Data.Migrations
{
    public partial class FixDepartmentFKandAddColumnToSalary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Currency",
                table: "Salary",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.DropForeignKey(
                name: "fk_105",
                table: "AspNetUsers");
            migrationBuilder.DropIndex(
                name: "fkIdx_105",
                table: "AspNetUsers");

            migrationBuilder.AddForeignKey(
                name: "fk_105",
                table: "AspNetUsers",
                column: "department_id",
                principalTable: "Departments",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.CreateIndex(
                name: "fkIdx_105",
                table: "AspNetUsers",
                column: "department_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Currency",
                table: "Salary");
        }
    }
}
