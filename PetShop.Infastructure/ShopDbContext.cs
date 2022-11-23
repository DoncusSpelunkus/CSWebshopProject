using Factory.Domain;
using Microsoft.EntityFrameworkCore;

namespace Factory.Infastructure
{
    public class ShopDbContext : DbContext
    {
        public ShopDbContext(DbContextOptions<ShopDbContext> opts) : base(opts)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .Property(p => p.ID)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Product>()
                .HasKey(c => new { ManFacId = c.ID });

        }

        public DbSet<Product> BoxTable { get; set; }
    }
}