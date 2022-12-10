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
            //Auto increment id for entities
            modelBuilder.Entity<Product>()
                .Property(p => p.ID)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<MainCategory>()
                .Property(f => f.RefID)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<SubCategory>()
                .Property(f => f.RefID)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<User>()
                .Property(id => id.Id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Specs>()
                .Property(s => s.ID)
                .ValueGeneratedOnAdd();


            //Setting keys
            modelBuilder.Entity<SpecsDescription>()
                .HasKey(sd => new { sd.ProductId, sd.SpecsId });
            modelBuilder.Entity<Product>()
                .HasKey(c => new { ManFacId = c.ID });
            modelBuilder.Entity<MainCategory>()
                .HasKey(c => new { ManFacId = c.RefID });
            modelBuilder.Entity<SubCategory>()
                .HasKey(c => new { ManFacId = c.RefID });
            modelBuilder.Entity<Rating>(r => r
                .HasKey(x => new { x.ProductId, x.UserId }));
          
                

            //Making foreign keys for the categories. The product stores a single category but the categories store multiple products
            modelBuilder.Entity<Product>()
                .HasOne<MainCategory>(p => p.MainCategoryObj)
                .WithMany(c => c.ProdList)
                .HasForeignKey(p => p.MainCategoryObjId);
            modelBuilder.Entity<Product>()
                .HasOne<SubCategory>(p => p.SubCategoryObj)
                .WithMany(c => c.ProdList)
                .HasForeignKey(p => p.SubCategoryObjId);
           
            
            // a rating has a single product but a product has multiple ratings
            modelBuilder.Entity<Rating>()
                .HasOne(r => r.Product)
                .WithMany(p => p.Ratings)
                .HasForeignKey(r => r.ProductId)
                .OnDelete(DeleteBehavior.ClientCascade);
            //a rating has one user, but a user has many different ratings.
            modelBuilder.Entity<Rating>()
                .HasOne(r => r.User)
                .WithMany(u => u.Ratings)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.ClientCascade);;
            // a user has many ratings, but a rating has one user
            modelBuilder.Entity<User>()
                .HasMany(u => u.Ratings)
                .WithOne(r => r.User);
            // a product has many ratings, but a rating has one product
            modelBuilder.Entity<Product>()
                .HasMany(p => p.Ratings)
                .WithOne(r => r.Product);
             
               
            modelBuilder.Entity<MainCategory>()
                .HasMany<Product>(mc => mc.ProdList)
                .WithOne(p => p.MainCategoryObj)
                .HasForeignKey(p => p.MainCategoryObjId);
            modelBuilder.Entity<SubCategory>()
                .HasMany<Product>(sc => sc.ProdList)
                .WithOne(p => p.SubCategoryObj)
                .HasForeignKey(p => p.SubCategoryObjId);
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
            modelBuilder.Entity<Specs>()
                .Ignore(s => s.SpecsDescriptions);
            modelBuilder.Entity<Product>()
                .Ignore(p => p.Ratings);
            modelBuilder.Entity<User>()
                .Ignore(u => u.Ratings);
        }
        public DbSet<SpecsDescription> SpecsDescriptionsTable { get; set; }
        public DbSet<Product> ProductTable { get; set; }
        public DbSet<MainCategory> MainCategoryTable { get; set; }
        public DbSet<SubCategory> SubCategoryTable { get; set; }
        public DbSet<Specs> SpecsTable { get; set; }
        public DbSet<Brand> BrandTable { get; set; }
        public DbSet<User> UserTable { get; set; }
        public DbSet<Rating> RatingsTable { get; set; }
    }

}