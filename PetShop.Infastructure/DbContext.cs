using Factory.Domain;
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
            //Auto increment
            modelBuilder.Entity<Product>()
                .Property(p => p.ID)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<MainCategory>()
                .Property(f => f.MainCategoryID)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<SubCategory>()
                .Property(f => f.SubCategoryID)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Brand>()
                .Property(b => b.Id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Specs>()
                .Property(s => s.ID)
                .ValueGeneratedOnAdd();

            //Setting keys
            modelBuilder.Entity<Product>()
                .HasKey(c => new { ManFacId = c.ID });
            modelBuilder.Entity<MainCategory>()
                .HasKey(c => new { ManFacId = c.MainCategoryID });
            modelBuilder.Entity<SubCategory>()
                .HasKey(c => new { ManFacId = c.SubCategoryID });
            modelBuilder.Entity<SpecsDescription>()
                .HasKey(sd => new { sd.ProductId, sd.SpecsId });
            
            
            
            modelBuilder.Entity<MainCategory>()
                .HasMany<Product>(mc => mc.ProdList)
                .WithOne(p => p.MainCategoryObj)
                .HasForeignKey(p => p.MainCategoryID);
            modelBuilder.Entity<SubCategory>()
                .HasMany<Product>(sc => sc.ProdList)
                .WithOne(p => p.SubCategoryObj)
                .HasForeignKey(p => p.SubCategoryID);

           
            

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
            modelBuilder.Entity<Product>()
                .Ignore(p => p.MainCategoryObj);
            modelBuilder.Entity<Product>()
                .Ignore(p => p.SubCategoryObj);
            modelBuilder.Entity<MainCategory>()
                .Ignore(c => c.ProdList);
            modelBuilder.Entity<SubCategory>()
                .Ignore(c => c.ProdList);
            modelBuilder.Entity<Brand>()
                .Ignore(c => c.ProdList);


        }
        public DbSet<SpecsDescription> SpecsDescriptionsTable { get; set; }
        public DbSet<Product> ProductTable { get; set; }
        public DbSet<MainCategory> MainCategoryTable { get; set; }
        public DbSet<SubCategory> SubCategoryTable { get; set; }
        public DbSet<Specs> SpecsTable { get; set; }
        public DbSet<Brand> BrandTable { get; set; }
    }

}