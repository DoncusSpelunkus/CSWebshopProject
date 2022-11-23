using Factory.Core;
using Microsoft.EntityFrameworkCore;

namespace Factory.Infastructure
{
    public class BoxDbContext : DbContext
    {
        public BoxDbContext(DbContextOptions<BoxDbContext> opts) : base(opts)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Box>()
                .Property(p => p.ManFacId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Box>()
                .HasKey(c => new { c.ManFacId });

        }

        public DbSet<Box> BoxTable { get; set; }
    }
}