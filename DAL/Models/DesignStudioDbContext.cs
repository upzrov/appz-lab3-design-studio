using Microsoft.EntityFrameworkCore;

namespace DAL.Models
{
    public class DesignStudioDbContext : DbContext
    {
        public DbSet<DesignService> DesignServices { get; set; }
        public DbSet<PortfolioItem> PortfolioItems { get; set; }
        public DbSet<Order> Orders { get; set; }

        public DesignStudioDbContext(DbContextOptions<DesignStudioDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
