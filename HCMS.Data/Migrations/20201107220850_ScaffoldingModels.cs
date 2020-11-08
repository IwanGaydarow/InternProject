using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace HCMS.Data.Migrations
{
    public partial class ScaffoldingModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "Trainings",
            //    columns: table => new
            //    {
            //        id = table.Column<int>(nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        tittle = table.Column<string>(maxLength: 80, nullable: false),
            //        description = table.Column<string>(nullable: false),
            //        created_on = table.Column<DateTime>(type: "date", nullable: false),
            //        modified_on = table.Column<DateTime>(type: "date", nullable: true),
            //        is_deleted = table.Column<bool>(nullable: false),
            //        delete_on = table.Column<DateTime>(type: "date", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Trainings", x => x.id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Departments",
            //    columns: table => new
            //    {
            //        id = table.Column<int>(nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        tittle = table.Column<string>(maxLength: 80, nullable: false),
            //        created_on = table.Column<DateTime>(type: "date", nullable: false),
            //        modified_on = table.Column<DateTime>(type: "date", nullable: true),
            //        is_deleted = table.Column<bool>(nullable: false),
            //        deleted_on = table.Column<DateTime>(type: "date", nullable: true),
            //        department_manager = table.Column<string>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Departments", x => x.id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "AppUser",
            //    columns: table => new
            //    {
            //        id = table.Column<string>(nullable: false),
            //        name = table.Column<string>(maxLength: 100, nullable: false),
            //        address = table.Column<string>(maxLength: 150, nullable: false),
            //        gender = table.Column<string>(maxLength: 10, nullable: false),
            //        created_on = table.Column<DateTime>(type: "date", nullable: false),
            //        modified_on = table.Column<DateTime>(type: "date", nullable: true),
            //        is_deleted = table.Column<bool>(nullable: false),
            //        deleted_on = table.Column<DateTime>(type: "date", nullable: true),
            //        department_id = table.Column<int>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_AppUser", x => x.id);
            //        table.ForeignKey(
            //            name: "fk_105",
            //            column: x => x.department_id,
            //            principalTable: "Departments",
            //            principalColumn: "id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Projects",
            //    columns: table => new
            //    {
            //        id = table.Column<int>(nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        tittle = table.Column<string>(maxLength: 80, nullable: false),
            //        description = table.Column<string>(nullable: false),
            //        estimated_work_hours = table.Column<int>(nullable: false),
            //        status = table.Column<bool>(nullable: false),
            //        created_on = table.Column<DateTime>(type: "date", nullable: false),
            //        modified_on = table.Column<DateTime>(type: "date", nullable: true),
            //        is_deleted = table.Column<bool>(nullable: false),
            //        deleted_on = table.Column<DateTime>(type: "date", nullable: true),
            //        department_id = table.Column<int>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Projects", x => x.id);
            //        table.ForeignKey(
            //            name: "FK_56",
            //            column: x => x.department_id,
            //            principalTable: "Departments",
            //            principalColumn: "id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Evaluations",
            //    columns: table => new
            //    {
            //        id = table.Column<int>(nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        value = table.Column<int>(nullable: false),
            //        notes = table.Column<string>(nullable: false),
            //        created_on = table.Column<DateTime>(type: "date", nullable: false),
            //        modified_on = table.Column<DateTime>(type: "date", nullable: true),
            //        is_deleted = table.Column<bool>(nullable: false),
            //        deleted_on = table.Column<DateTime>(type: "date", nullable: true),
            //        user_id = table.Column<string>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Evaluations", x => x.id);
            //        table.ForeignKey(
            //            name: "FK_85",
            //            column: x => x.user_id,
            //            principalTable: "AppUser",
            //            principalColumn: "id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Salary",
            //    columns: table => new
            //    {
            //        id = table.Column<int>(nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        salary = table.Column<decimal>(type: "money", nullable: false),
            //        created_on = table.Column<DateTime>(type: "date", nullable: false),
            //        modified_on = table.Column<DateTime>(type: "date", nullable: true),
            //        user_id = table.Column<string>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Salary", x => x.id);
            //        table.ForeignKey(
            //            name: "FK_67",
            //            column: x => x.user_id,
            //            principalTable: "AppUser",
            //            principalColumn: "id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "TrainingsUsers",
            //    columns: table => new
            //    {
            //        user_id = table.Column<string>(nullable: false),
            //        training_id = table.Column<int>(nullable: false),
            //        created_on = table.Column<DateTime>(type: "date", nullable: false),
            //        modified_on = table.Column<DateTime>(type: "date", nullable: true),
            //        is_deleted = table.Column<bool>(nullable: false),
            //        deleted_on = table.Column<DateTime>(type: "date", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_trainingsusers", x => new { x.user_id, x.training_id });
            //        table.ForeignKey(
            //            name: "FK_45",
            //            column: x => x.training_id,
            //            principalTable: "Trainings",
            //            principalColumn: "id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_42",
            //            column: x => x.user_id,
            //            principalTable: "AppUser",
            //            principalColumn: "id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Vacations",
            //    columns: table => new
            //    {
            //        id = table.Column<int>(nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        tittle = table.Column<string>(maxLength: 80, nullable: false),
            //        from_date = table.Column<DateTime>(type: "date", nullable: false),
            //        to_date = table.Column<DateTime>(type: "date", nullable: false),
            //        status = table.Column<bool>(nullable: true),
            //        created_on = table.Column<DateTime>(type: "date", nullable: false),
            //        modified_on = table.Column<DateTime>(type: "date", nullable: true),
            //        is_deleted = table.Column<bool>(nullable: false),
            //        deleted_on = table.Column<DateTime>(type: "date", nullable: true),
            //        user_id = table.Column<string>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Vacations", x => x.id);
            //        table.ForeignKey(
            //            name: "FK_112",
            //            column: x => x.user_id,
            //            principalTable: "AppUser",
            //            principalColumn: "id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ProjectTasks",
            //    columns: table => new
            //    {
            //        id = table.Column<int>(nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        tittle = table.Column<string>(maxLength: 50, nullable: false),
            //        description = table.Column<string>(nullable: false),
            //        estimated_work_hours = table.Column<int>(nullable: false),
            //        status = table.Column<bool>(nullable: false),
            //        creaded_on = table.Column<DateTime>(type: "date", nullable: false),
            //        modified_on = table.Column<DateTime>(type: "date", nullable: true),
            //        is_deleted = table.Column<bool>(nullable: false),
            //        deleted_on = table.Column<DateTime>(type: "date", nullable: true),
            //        project_id = table.Column<int>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ProjectTasks", x => x.id);
            //        table.ForeignKey(
            //            name: "FK_93",
            //            column: x => x.project_id,
            //            principalTable: "Projects",
            //            principalColumn: "id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "UsersTasks",
            //    columns: table => new
            //    {
            //        project_task_id = table.Column<int>(nullable: false),
            //        user_id = table.Column<string>(nullable: false),
            //        created_on = table.Column<DateTime>(type: "date", nullable: false),
            //        modified_on = table.Column<DateTime>(type: "date", nullable: true),
            //        is_deleted = table.Column<bool>(nullable: false),
            //        deleted_on = table.Column<DateTime>(type: "date", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_userstasks", x => new { x.project_task_id, x.user_id });
            //        table.ForeignKey(
            //            name: "FK_97",
            //            column: x => x.project_task_id,
            //            principalTable: "ProjectTasks",
            //            principalColumn: "id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_101",
            //            column: x => x.user_id,
            //            principalTable: "AppUser",
            //            principalColumn: "id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "fkIdx_105",
            //    table: "AppUser",
            //    column: "department_id");

            //migrationBuilder.CreateIndex(
            //    name: "fkIdx_29",
            //    table: "Departments",
            //    column: "department_manager");

            //migrationBuilder.CreateIndex(
            //    name: "fkIdx_85",
            //    table: "Evaluations",
            //    column: "user_id");

            //migrationBuilder.CreateIndex(
            //    name: "fkIdx_56",
            //    table: "Projects",
            //    column: "department_id");

            //migrationBuilder.CreateIndex(
            //    name: "fkIdx_93",
            //    table: "ProjectTasks",
            //    column: "project_id");

            //migrationBuilder.CreateIndex(
            //    name: "fkIdx_67",
            //    table: "Salary",
            //    column: "user_id");

            //migrationBuilder.CreateIndex(
            //    name: "fkIdx_45",
            //    table: "TrainingsUsers",
            //    column: "training_id");

            //migrationBuilder.CreateIndex(
            //    name: "fkIdx_42",
            //    table: "TrainingsUsers",
            //    column: "user_id");

            //migrationBuilder.CreateIndex(
            //    name: "fkIdx_97",
            //    table: "UsersTasks",
            //    column: "project_task_id");

            //migrationBuilder.CreateIndex(
            //    name: "fkIdx_101",
            //    table: "UsersTasks",
            //    column: "user_id");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Vacations_user_id",
            //    table: "Vacations",
            //    column: "user_id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_29",
            //    table: "Departments",
            //    column: "department_manager",
            //    principalTable: "AppUser",
            //    principalColumn: "id",
            //    onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_105",
                table: "AppUser");

            migrationBuilder.DropTable(
                name: "Evaluations");

            migrationBuilder.DropTable(
                name: "Salary");

            migrationBuilder.DropTable(
                name: "TrainingsUsers");

            migrationBuilder.DropTable(
                name: "UsersTasks");

            migrationBuilder.DropTable(
                name: "Vacations");

            migrationBuilder.DropTable(
                name: "Trainings");

            migrationBuilder.DropTable(
                name: "ProjectTasks");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "AppUser");
        }
    }
}
