﻿// <auto-generated />
using System;
using HCMS.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace HCMS.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("HCMS.Data.Models.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnName("id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnName("address")
                        .HasColumnType("character varying(150)")
                        .HasMaxLength(150);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnName("created_on")
                        .HasColumnType("date");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnName("deleted_on")
                        .HasColumnType("date");

                    b.Property<int?>("DepartmentId")
                        .HasColumnName("department_id")
                        .HasColumnType("integer");

                    b.Property<string>("Email")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnName("gender")
                        .HasColumnType("character varying(10)")
                        .HasMaxLength(10);

                    b.Property<bool>("IsDeleted")
                        .HasColumnName("is_deleted")
                        .HasColumnType("boolean");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnName("modified_on")
                        .HasColumnType("date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId")
                        .HasName("fkIdx_105");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("HCMS.Data.Models.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnName("created_on")
                        .HasColumnType("date");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnName("deleted_on")
                        .HasColumnType("date");

                    b.Property<string>("Email")
                        .HasColumnName("email")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.Property<string>("FullAddres")
                        .IsRequired()
                        .HasColumnName("full_addres")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnName("is_deleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnName("modified_on")
                        .HasColumnType("date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnName("phonenumber")
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("HCMS.Data.Models.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("CompanyId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnName("created_on")
                        .HasColumnType("date");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnName("deleted_on")
                        .HasColumnType("date");

                    b.Property<string>("DepartmentManager")
                        .HasColumnName("department_manager")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnName("is_deleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnName("modified_on")
                        .HasColumnType("date");

                    b.Property<string>("Tittle")
                        .IsRequired()
                        .HasColumnName("tittle")
                        .HasColumnType("character varying(80)")
                        .HasMaxLength(80);

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("DepartmentManager")
                        .HasName("fkIdx_29");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("HCMS.Data.Models.Evaluations", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnName("created_on")
                        .HasColumnType("date");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnName("deleted_on")
                        .HasColumnType("date");

                    b.Property<bool>("IsDeleted")
                        .HasColumnName("is_deleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnName("modified_on")
                        .HasColumnType("date");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasColumnName("notes")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnName("user_id")
                        .HasColumnType("text");

                    b.Property<int>("Value")
                        .HasColumnName("value")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .HasName("fkIdx_85");

                    b.ToTable("Evaluations");
                });

            modelBuilder.Entity("HCMS.Data.Models.ProjectTasks", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnName("created_on")
                        .HasColumnType("date");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnName("deleted_on")
                        .HasColumnType("date");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnName("description")
                        .HasColumnType("text");

                    b.Property<int>("EstimatedWorkHours")
                        .HasColumnName("estimated_work_hours")
                        .HasColumnType("integer");

                    b.Property<bool>("IsDeleted")
                        .HasColumnName("is_deleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnName("modified_on")
                        .HasColumnType("date");

                    b.Property<int>("ProjectId")
                        .HasColumnName("project_id")
                        .HasColumnType("integer");

                    b.Property<bool>("Status")
                        .HasColumnName("status")
                        .HasColumnType("boolean");

                    b.Property<string>("Tittle")
                        .IsRequired()
                        .HasColumnName("tittle")
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("ProjectId")
                        .HasName("fkIdx_93");

                    b.ToTable("ProjectTasks");
                });

            modelBuilder.Entity("HCMS.Data.Models.Projects", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnName("created_on")
                        .HasColumnType("date");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnName("deleted_on")
                        .HasColumnType("date");

                    b.Property<int>("DepartmentId")
                        .HasColumnName("department_id")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnName("description")
                        .HasColumnType("text");

                    b.Property<int>("EstimatedWorkHours")
                        .HasColumnName("estimated_work_hours")
                        .HasColumnType("integer");

                    b.Property<bool>("IsDeleted")
                        .HasColumnName("is_deleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnName("modified_on")
                        .HasColumnType("date");

                    b.Property<bool>("Status")
                        .HasColumnName("status")
                        .HasColumnType("boolean");

                    b.Property<string>("Tittle")
                        .IsRequired()
                        .HasColumnName("tittle")
                        .HasColumnType("character varying(80)")
                        .HasMaxLength(80);

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId")
                        .HasName("fkIdx_56");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("HCMS.Data.Models.Salary", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnName("created_on")
                        .HasColumnType("date");

                    b.Property<int>("Currency")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnName("deleted_on")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("EffectiveTo")
                        .HasColumnName("effective_to")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnName("is_deleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnName("modified_on")
                        .HasColumnType("date");

                    b.Property<int>("Periodicity")
                        .HasColumnName("periodicity")
                        .HasColumnType("integer");

                    b.Property<decimal>("Salary1")
                        .HasColumnName("salary")
                        .HasColumnType("money");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnName("user_id")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .HasName("fkIdx_67");

                    b.ToTable("Salary");
                });

            modelBuilder.Entity("HCMS.Data.Models.Trainings", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnName("created_on")
                        .HasColumnType("date");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnName("delete_on")
                        .HasColumnType("date");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnName("description")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnName("is_deleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnName("modified_on")
                        .HasColumnType("date");

                    b.Property<string>("Tittle")
                        .IsRequired()
                        .HasColumnName("tittle")
                        .HasColumnType("character varying(80)")
                        .HasMaxLength(80);

                    b.HasKey("Id");

                    b.ToTable("Trainings");
                });

            modelBuilder.Entity("HCMS.Data.Models.TrainingsUsers", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnName("user_id")
                        .HasColumnType("text");

                    b.Property<int>("TrainingId")
                        .HasColumnName("training_id")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnName("created_on")
                        .HasColumnType("date");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnName("deleted_on")
                        .HasColumnType("date");

                    b.Property<bool>("IsDeleted")
                        .HasColumnName("is_deleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnName("modified_on")
                        .HasColumnType("date");

                    b.HasKey("UserId", "TrainingId")
                        .HasName("PK_trainingsusers");

                    b.HasIndex("TrainingId")
                        .HasName("fkIdx_45");

                    b.HasIndex("UserId")
                        .HasName("fkIdx_42");

                    b.ToTable("TrainingsUsers");
                });

            modelBuilder.Entity("HCMS.Data.Models.UsersTasks", b =>
                {
                    b.Property<int>("ProjectTaskId")
                        .HasColumnName("project_task_id")
                        .HasColumnType("integer");

                    b.Property<string>("UserId")
                        .HasColumnName("user_id")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnName("created_on")
                        .HasColumnType("date");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnName("deleted_on")
                        .HasColumnType("date");

                    b.Property<bool>("IsDeleted")
                        .HasColumnName("is_deleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnName("modified_on")
                        .HasColumnType("date");

                    b.HasKey("ProjectTaskId", "UserId")
                        .HasName("PK_userstasks");

                    b.HasIndex("ProjectTaskId")
                        .HasName("fkIdx_97");

                    b.HasIndex("UserId")
                        .HasName("fkIdx_101");

                    b.ToTable("UsersTasks");
                });

            modelBuilder.Entity("HCMS.Data.Models.Vacations", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnName("created_on")
                        .HasColumnType("date");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnName("deleted_on")
                        .HasColumnType("date");

                    b.Property<DateTime>("FromDate")
                        .HasColumnName("from_date")
                        .HasColumnType("date");

                    b.Property<bool>("IsDeleted")
                        .HasColumnName("is_deleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnName("modified_on")
                        .HasColumnType("date");

                    b.Property<bool?>("Status")
                        .HasColumnName("status")
                        .HasColumnType("boolean");

                    b.Property<string>("Tittle")
                        .IsRequired()
                        .HasColumnName("tittle")
                        .HasColumnType("character varying(80)")
                        .HasMaxLength(80);

                    b.Property<DateTime>("ToDate")
                        .HasColumnName("to_date")
                        .HasColumnType("date");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnName("user_id")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Vacations");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("HCMS.Data.Models.AppUser", b =>
                {
                    b.HasOne("HCMS.Data.Models.Department", "Department")
                        .WithMany("Employess")
                        .HasForeignKey("DepartmentId")
                        .HasConstraintName("fk_105");
                });

            modelBuilder.Entity("HCMS.Data.Models.Department", b =>
                {
                    b.HasOne("HCMS.Data.Models.Company", "Company")
                        .WithMany("Departments")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HCMS.Data.Models.AppUser", "DepartmentManagerNavigation")
                        .WithMany("Departments")
                        .HasForeignKey("DepartmentManager")
                        .HasConstraintName("FK_29");
                });

            modelBuilder.Entity("HCMS.Data.Models.Evaluations", b =>
                {
                    b.HasOne("HCMS.Data.Models.AppUser", "User")
                        .WithMany("Evaluations")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_85")
                        .IsRequired();
                });

            modelBuilder.Entity("HCMS.Data.Models.ProjectTasks", b =>
                {
                    b.HasOne("HCMS.Data.Models.Projects", "Project")
                        .WithMany("ProjectTasks")
                        .HasForeignKey("ProjectId")
                        .HasConstraintName("FK_93")
                        .IsRequired();
                });

            modelBuilder.Entity("HCMS.Data.Models.Projects", b =>
                {
                    b.HasOne("HCMS.Data.Models.Department", "Department")
                        .WithMany("Projects")
                        .HasForeignKey("DepartmentId")
                        .HasConstraintName("FK_56")
                        .IsRequired();
                });

            modelBuilder.Entity("HCMS.Data.Models.Salary", b =>
                {
                    b.HasOne("HCMS.Data.Models.AppUser", "User")
                        .WithMany("Salary")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_67")
                        .IsRequired();
                });

            modelBuilder.Entity("HCMS.Data.Models.TrainingsUsers", b =>
                {
                    b.HasOne("HCMS.Data.Models.Trainings", "Training")
                        .WithMany("TrainingsUsers")
                        .HasForeignKey("TrainingId")
                        .HasConstraintName("FK_45")
                        .IsRequired();

                    b.HasOne("HCMS.Data.Models.AppUser", "User")
                        .WithMany("TrainingsUsers")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_42")
                        .IsRequired();
                });

            modelBuilder.Entity("HCMS.Data.Models.UsersTasks", b =>
                {
                    b.HasOne("HCMS.Data.Models.ProjectTasks", "ProjectTask")
                        .WithMany("UsersTasks")
                        .HasForeignKey("ProjectTaskId")
                        .HasConstraintName("FK_97")
                        .IsRequired();

                    b.HasOne("HCMS.Data.Models.AppUser", "User")
                        .WithMany("UsersTasks")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_101")
                        .IsRequired();
                });

            modelBuilder.Entity("HCMS.Data.Models.Vacations", b =>
                {
                    b.HasOne("HCMS.Data.Models.AppUser", "User")
                        .WithMany("Vacations")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_112")
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("HCMS.Data.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("HCMS.Data.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HCMS.Data.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("HCMS.Data.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
