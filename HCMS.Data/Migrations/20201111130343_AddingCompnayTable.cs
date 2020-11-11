using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace HCMS.Data.Migrations
{
    public partial class AddingCompnayTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Departments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(nullable: false),
                    email = table.Column<string>(maxLength: 256, nullable: true),
                    phonenumber = table.Column<string>(maxLength: 50, nullable: true),
                    full_addres = table.Column<int>(nullable: false),
                    created_on = table.Column<DateTime>(type: "date", nullable: false),
                    modified_on = table.Column<DateTime>(type: "date", nullable: true),
                    is_deleted = table.Column<bool>(nullable: false),
                    deleted_on = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Departments_CompanyId",
                table: "Departments",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Companies_CompanyId",
                table: "Departments",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Companies_CompanyId",
                table: "Departments");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Departments_CompanyId",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Departments");
        }
    }
}
