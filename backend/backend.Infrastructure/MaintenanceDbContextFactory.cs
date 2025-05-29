using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace backend.Infrastructure.Data
{
    public class MaintenanceDbContextFactory : IDesignTimeDbContextFactory<MaintenanceDbContext>
    {
        public MaintenanceDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MaintenanceDbContext>();

            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=TruckoomDb;Trusted_Connection=True;");

            return new MaintenanceDbContext(optionsBuilder.Options);
        }
    }
}
