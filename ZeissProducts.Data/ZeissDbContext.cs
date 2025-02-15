using Microsoft.EntityFrameworkCore;
using ZeissProducts.Data.Models;

namespace ZeissProducts.Data
{
    public class ZeissDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public ZeissDbContext(DbContextOptions<ZeissDbContext> dbContext) : base(dbContext)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                    .Property(p => p.Id)
                    .UseIdentityColumn(100000, 1);
        }
    }
}
