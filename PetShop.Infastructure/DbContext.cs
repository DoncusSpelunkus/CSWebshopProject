using PetShop.Domain;
using Microsoft.EntityFrameworkCore;

namespace PetShop.Infastructure
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> opts) : base(opts)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .Property(p => p.ID)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Specs>()
                .Property(s => s.ID)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<SpecsDescription>()
                .Property(sd => sd.ID)
                .ValueGeneratedOnAdd();


            modelBuilder.Entity<SpecsDescription>()
                .HasOne(sd => sd.Product)
                .WithMany(p => p.SpecsDescriptions)
                .HasForeignKey(sd => sd.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SpecsDescription>()
                .HasOne(sd => sd.Specs)
                .WithMany(s => s.SpecsDescriptions)
                .HasForeignKey(sd => sd.SpecsId)
                .OnDelete(DeleteBehavior.Cascade);

        }

        public DbSet<Product> ProductTable { get; set; }
        public DbSet<Specs> SpecsTable { get; set; }
    }
}