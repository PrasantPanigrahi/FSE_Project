using DataAccess.Migrations;
using PM.Models;
using System.Data.Entity;
using System.IO;

namespace PM.DAL
{
    public partial class PMDbContext : DbContext
    {
       
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<ParentTask> ParentTasks { get; set; }

        public PMDbContext() :
           base("ConnectionString")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<PMDbContext, Configuration>());
        }

        public static PMDbContext Create()
        {
            return new PMDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasKey<int>(s => s.Id);

            modelBuilder.Entity<ParentTask>().HasKey<int>(s => s.Id);

            modelBuilder.Entity<Models.Task>()
                .HasOptional<ParentTask>(s => s.ParentTask)
                .WithMany(p => p.Tasks)
                .HasForeignKey<int?>(t => t.ParentTaskId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Project>()
              .HasRequired<User>(p => p.Manager)
              .WithMany(u => u.Projects)
              .HasForeignKey<int>(p => p.ManagerId)
              .WillCascadeOnDelete(false);
        }
    }
}