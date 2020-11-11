using Microsoft.EntityFrameworkCore.Migrations;

namespace HCMS.Data.Migrations
{
    public partial class ChangeColumnName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "creaded_on",
                table: "ProjectTasks",
                newName: "created_on");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "created_on",
                table: "ProjectTasks",
                newName: "creaded_on");
        }
    }
}
