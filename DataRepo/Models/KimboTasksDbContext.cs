using Microsoft.EntityFrameworkCore;

namespace DataRepo.Models
{

    public class KimboTasksDbContext : DbContext
    {
        public KimboTasksDbContext(DbContextOptions<KimboTasksDbContext> options)
            : base(options) { }

        public DbSet<KimboTask> KimboTasks { get; set; }
    }
}
