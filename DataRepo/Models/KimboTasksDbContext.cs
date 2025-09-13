using Microsoft.EntityFrameworkCore;

namespace DataRepo.Models
{

    public class KimboTasksDbContext : DbContext
    {
        public KimboTasksDbContext(DbContextOptions<KimboTasksDbContext> options)
            : base(options) { }

        public DbSet<KimboTask> KimboTasks { get; set; }
        public DbSet<TaskEffort> TaskEfforts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed initial data
            modelBuilder.Entity<TaskEffort>().HasData(
                new TaskEffort { Id = 1, Text = "Low" },
                new TaskEffort { Id = 2, Text = "Medium" },
                new TaskEffort { Id = 3, Text = "High" }
            );
        }
    }
}
