using Microsoft.EntityFrameworkCore.Migrations;

namespace HCMS.Data.Migrations
{
    public partial class CreateRelationsBetweenCompanyAndTrainings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Trainings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Trainings_CompanyId",
                table: "Trainings",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trainings_Companies_CompanyId",
                table: "Trainings",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trainings_Companies_CompanyId",
                table: "Trainings");

            migrationBuilder.DropIndex(
                name: "IX_Trainings_CompanyId",
                table: "Trainings");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Trainings");
        }
    }
}
