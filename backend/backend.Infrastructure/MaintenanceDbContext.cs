using backend.Domain.Models;
using Microsoft.EntityFrameworkCore;
using DomainTask = backend.Domain.Models.Task;

namespace backend.Infrastructure.Data
{
    public class MaintenanceDbContext : DbContext
    {
        public MaintenanceDbContext(DbContextOptions<MaintenanceDbContext> options) : base(options)
        {
        }

        public DbSet<Service> Services { get; set; }
        public DbSet<DomainTask> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure one-to-many relationship between Service and Tasks
            modelBuilder.Entity<Service>()
                .HasMany(s => s.Tasks)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            // Optionally, configure other constraints if needed
        }
    }
}
