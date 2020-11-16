using Microsoft.EntityFrameworkCore.Migrations;

namespace HCMS.Data.Migrations
{
    public partial class FixCompaniesAdressColumnType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "full_addres",
                table: "Companies",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "full_addres",
                table: "Companies",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
