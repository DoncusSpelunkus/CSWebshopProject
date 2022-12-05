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
                .HasKey(sd => new { sd.ProductId, sd.SpecsId });
            
            

            // specsDescription has one product, one product has many specsDecription.
            modelBuilder.Entity<SpecsDescription>()
                .HasOne(sd => sd.Product)
                .WithMany(p => p.SpecsDescriptions)
                .HasForeignKey(sd => sd.ProductId)
                .OnDelete(DeleteBehavior.ClientCascade);
            // one specs has many specsDescription, and one specsDescription has one specs
            modelBuilder.Entity<SpecsDescription>()
                .HasOne(sd => sd.Specs)
                .WithMany(s => s.SpecsDescriptions)
                .HasForeignKey(sd => sd.SpecsId)
                .OnDelete(DeleteBehavior.ClientCascade);
            
            /*
             * a product has many specsDescription and a specsDescription has one product
             */
            modelBuilder.Entity<Product>()
                .HasMany(p => p.SpecsDescriptions)
                .WithOne(sd => sd.Product).OnDelete(DeleteBehavior.ClientCascade);
            /*
            * a specs has many specsDescription and a specsDescription has one specs
            */
            modelBuilder.Entity<Specs>()
                .HasMany(s => s.SpecsDescriptions)
                .WithOne(sd => sd.Specs).OnDelete(DeleteBehavior.ClientCascade);

            //Dont auto include 
            modelBuilder.Entity<SpecsDescription>()
                .Ignore(sd => sd.Product);
            modelBuilder.Entity<SpecsDescription>()
                .Ignore(sd => sd.Specs);

            

        }
        public DbSet<SpecsDescription> SpecsDescriptionsTable { get; set; }
        public DbSet<Product> ProductTable { get; set; }
        public DbSet<Specs> SpecsTable { get; set; }
    }
}