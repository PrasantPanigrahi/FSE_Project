using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PM.Models;
using System.IO;

namespace PM.DAL
{
    public partial class PMDbContext : DbContext
    {
       
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<ParentTask> ParentTasks { get; set; }

        public static PMDbContext Create(string connStr= null)
        {
            if (string.IsNullOrEmpty(connStr)) connStr = GetConnectionString();
            var optionsBuilder = new DbContextOptionsBuilder<PMDbContext>();
            optionsBuilder.UseSqlServer(connStr);

            return new PMDbContext(optionsBuilder.Options);
        }

        private static string GetConnectionString()
        {
            var builder = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            var configuration = builder.Build();

            //set connection string
            var connectionString = configuration.GetConnectionString("defaultConnection");
            return connectionString;
        }

        public PMDbContext(DbContextOptions<PMDbContext> options)
          : base(options)
        {
            //run migrations
            Database.Migrate();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasKey(s => s.Id);

            modelBuilder.Entity<User>()
                .Property(s => s.Id)
                .UseSqlServerIdentityColumn();

            modelBuilder.Entity<ParentTask>().HasKey(s => s.Id);

            modelBuilder.Entity<Task>()
                        .HasOne(s => s.ParentTask)
                        .WithMany(p=>p.Tasks)
                        .HasForeignKey(t => t.ParentTaskId)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Project>()
                     .HasMany(s => s.Tasks)
                     .WithOne(p => p.Project)
                     .HasForeignKey(t => t.ProjectId)
                     .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Project>()
                        .HasOne(p => p.Manager)
                        .WithMany(u => u.Projects)
                        .HasForeignKey(p => p.ManagerId)
                        .OnDelete(DeleteBehavior.Cascade);

            //initial data seeding
            modelBuilder.SeedData();
        }
    }
}