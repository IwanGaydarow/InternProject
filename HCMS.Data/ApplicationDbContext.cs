namespace HCMS.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using System.Security.Cryptography.X509Certificates;

    public partial class ApplicationDbContext : IdentityDbContext<AppUser, IdentityRole, string>
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AppUser> AppUser { get; set; }

        public virtual DbSet<Department> Departments { get; set; }

        public virtual DbSet<Evaluations> Evaluations { get; set; }

        public virtual DbSet<ProjectTasks> ProjectTasks { get; set; }

        public virtual DbSet<Project> Projects { get; set; }

        public virtual DbSet<Salary> Salary { get; set; }

        public virtual DbSet<Trainings> Trainings { get; set; }

        public virtual DbSet<TrainingsUsers> TrainingsUsers { get; set; }

        public virtual DbSet<UsersTasks> UsersTasks { get; set; }

        public virtual DbSet<Vacations> Vacations { get; set; }

        public virtual DbSet<Company> Companies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseNpgsql("Host=localhost;Database=HCMS Database;Username=postgres;Password=postgreadmin");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppUser>(entity =>
            {
                entity.HasIndex(e => e.DepartmentId)
                    .HasName("fkIdx_105");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnName("address")
                    .HasMaxLength(150);

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("created_on")
                    .HasColumnType("date");

                entity.Property(e => e.DeletedOn)
                    .HasColumnName("deleted_on")
                    .HasColumnType("date");

                entity.Property(e => e.DepartmentId).HasColumnName("department_id");

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasColumnName("gender")
                    .HasMaxLength(10);

                entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnName("modified_on")
                    .HasColumnType("date");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(100);

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Employess)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("fk_105");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasIndex(e => e.DepartmentManager)
                    .HasName("fkIdx_29");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("created_on")
                    .HasColumnType("date");

                entity.Property(e => e.DeletedOn)
                    .HasColumnName("deleted_on")
                    .HasColumnType("date");

                entity.Property(e => e.DepartmentManager).HasColumnName("department_manager");

                entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnName("modified_on")
                    .HasColumnType("date");

                entity.Property(e => e.Tittle)
                    .IsRequired()
                    .HasColumnName("tittle")
                    .HasMaxLength(80);

                entity.HasOne(d => d.DepartmentManagerNavigation)
                    .WithMany(p => p.Departments)
                    .HasForeignKey(d => d.DepartmentManager)
                    .HasConstraintName("FK_29");
            });

            modelBuilder.Entity<Evaluations>(entity =>
            {
                entity.HasIndex(e => e.UserId)
                    .HasName("fkIdx_85");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("created_on")
                    .HasColumnType("date");

                entity.Property(e => e.DeletedOn)
                    .HasColumnName("deleted_on")
                    .HasColumnType("date");

                entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnName("modified_on")
                    .HasColumnType("date");

                entity.Property(e => e.Notes)
                    .IsRequired()
                    .HasColumnName("notes");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasColumnName("user_id");

                entity.Property(e => e.Value).HasColumnName("value");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Evaluations)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_85");
            });

            modelBuilder.Entity<ProjectTasks>(entity =>
            {
                entity.HasIndex(e => e.ProjectId)
                    .HasName("fkIdx_93");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("created_on")
                    .HasColumnType("date");

                entity.Property(e => e.DeletedOn)
                    .HasColumnName("deleted_on")
                    .HasColumnType("date");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description");

                entity.Property(e => e.EstimatedWorkHours).HasColumnName("estimated_work_hours");

                entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnName("modified_on")
                    .HasColumnType("date");

                entity.Property(e => e.ProjectId).HasColumnName("project_id");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Tittle)
                    .IsRequired()
                    .HasColumnName("tittle")
                    .HasMaxLength(50);

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ProjectTasks)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_93");
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.HasIndex(e => e.DepartmentId)
                    .HasName("fkIdx_56");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("created_on")
                    .HasColumnType("date");

                entity.Property(e => e.DeletedOn)
                    .HasColumnName("deleted_on")
                    .HasColumnType("date");

                entity.Property(e => e.DepartmentId).HasColumnName("department_id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description");

                entity.Property(e => e.EstimatedWorkHours).HasColumnName("estimated_work_hours");

                entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnName("modified_on")
                    .HasColumnType("date");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Tittle)
                    .IsRequired()
                    .HasColumnName("tittle")
                    .HasMaxLength(80);

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_56");
            });

            modelBuilder.Entity<Salary>(entity =>
            {
                entity.HasIndex(e => e.UserId)
                    .HasName("fkIdx_67");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("created_on")
                    .HasColumnType("date");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnName("modified_on")
                    .HasColumnType("date");

                entity.Property(e => e.Salary1)
                    .HasColumnName("salary")
                    .HasColumnType("money");

                entity.Property(e => e.Periodicity)
                .HasColumnName("periodicity");

                entity.Property(e => e.EffectiveTo)
                .HasColumnName("effective_to");

                entity.Property(e => e.IsDeleted)
                .HasColumnName("is_deleted");

                entity.Property(e => e.DeletedOn)
                .HasColumnName("deleted_on");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Salary)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_67");
            });

            modelBuilder.Entity<Trainings>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("created_on")
                    .HasColumnType("date");

                entity.Property(e => e.DeletedOn)
                    .HasColumnName("delete_on")
                    .HasColumnType("date");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description");

                entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnName("modified_on")
                    .HasColumnType("date");

                entity.Property(e => e.Tittle)
                    .IsRequired()
                    .HasColumnName("tittle")
                    .HasMaxLength(80);
            });

            modelBuilder.Entity<TrainingsUsers>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.TrainingId })
                    .HasName("PK_trainingsusers");

                entity.HasIndex(e => e.TrainingId)
                    .HasName("fkIdx_45");

                entity.HasIndex(e => e.UserId)
                    .HasName("fkIdx_42");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.TrainingId).HasColumnName("training_id");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("created_on")
                    .HasColumnType("date");

                entity.Property(e => e.DeletedOn)
                    .HasColumnName("deleted_on")
                    .HasColumnType("date");

                entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnName("modified_on")
                    .HasColumnType("date");

                entity.HasOne(d => d.Training)
                    .WithMany(p => p.TrainingsUsers)
                    .HasForeignKey(d => d.TrainingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_45");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TrainingsUsers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_42");
            });

            modelBuilder.Entity<UsersTasks>(entity =>
            {
                entity.HasKey(e => new { e.ProjectTaskId, e.UserId })
                    .HasName("PK_userstasks");

                entity.HasIndex(e => e.ProjectTaskId)
                    .HasName("fkIdx_97");

                entity.HasIndex(e => e.UserId)
                    .HasName("fkIdx_101");

                entity.Property(e => e.ProjectTaskId).HasColumnName("project_task_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("created_on")
                    .HasColumnType("date");

                entity.Property(e => e.DeletedOn)
                    .HasColumnName("deleted_on")
                    .HasColumnType("date");

                entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnName("modified_on")
                    .HasColumnType("date");

                entity.HasOne(d => d.ProjectTask)
                    .WithMany(p => p.UsersTasks)
                    .HasForeignKey(d => d.ProjectTaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_97");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UsersTasks)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_101");
            });

            modelBuilder.Entity<Vacations>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("created_on")
                    .HasColumnType("date");

                entity.Property(e => e.DeletedOn)
                    .HasColumnName("deleted_on")
                    .HasColumnType("date");

                entity.Property(e => e.FromDate)
                    .HasColumnName("from_date")
                    .HasColumnType("date");

                entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnName("modified_on")
                    .HasColumnType("date");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Tittle)
                    .IsRequired()
                    .HasColumnName("tittle")
                    .HasMaxLength(80);

                entity.Property(e => e.ToDate)
                    .HasColumnName("to_date")
                    .HasColumnType("date");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Vacations)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_112");
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasColumnName("name");

                entity.Property(e => e.Email)
                    .HasColumnName("email");

                entity.Property(e => e.PhoneNumber)
                .HasColumnName("phonenumber");

                entity.Property(e => e.FullAddres)
                .HasColumnName("full_addres");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("created_on")
                    .HasColumnType("date");

                entity.Property(e => e.DeletedOn)
                    .HasColumnName("deleted_on")
                    .HasColumnType("date");

                entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnName("modified_on")
                    .HasColumnType("date");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
