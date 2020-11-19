using Microsoft.EntityFrameworkCore.Migrations;

namespace HCMS.Data.Migrations
{
    public partial class ChangeDepartmentIdToBeRequierdAppUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_105",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "department_id",
                table: "AspNetUsers",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "fk_105",
                table: "AspNetUsers",
                column: "department_id",
                principalTable: "Departments",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_105",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "department_id",
                table: "AspNetUsers",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "fk_105",
                table: "AspNetUsers",
                column: "department_id",
                principalTable: "Departments",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
