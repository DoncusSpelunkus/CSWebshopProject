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
            modelBuilder.Entity<User>()
                .Property(id => id.Id)
                .ValueGeneratedOnAdd(); 
            
            
            //Setting keys
            modelBuilder.Entity<SpecsDescription>()
                .HasKey(sd => new { sd.ProductId, sd.SpecsId });
            modelBuilder.Entity<Product>()
                .HasKey(c => new { ManFacId = c.ID });
            modelBuilder.Entity<MainCategory>()
                .HasKey(c => new { ManFacId = c.MainCategoryID });
            modelBuilder.Entity<SubCategory>()
                .HasKey(c => new { ManFacId = c.SubCategoryID });
            modelBuilder.Entity<Brand>()
                .HasKey(c => new { ManFacId = c.Id });
            modelBuilder.Entity<Rating>(r => r
                .HasKey(r => new { r.ProductId, r.UserId }));
            modelBuilder.Entity<Order>()
                .HasKey(o => new { o.ProductId, o.UserId, o.OrderId });

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
            
            // navigation expressions and foreign keys for order
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Product)
                .WithMany(p => p.CurrentOrderList)
                .HasForeignKey(o => o.ProductId)
                .OnDelete(DeleteBehavior.ClientCascade);
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Product)
                .WithMany(p => p.CurrentOrderList)
                .HasForeignKey(o => o.productName)
                .OnDelete(DeleteBehavior.ClientCascade);
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Product)
                .WithMany(p => p.CurrentOrderList)
                .HasForeignKey(o => o.productImage)
                .OnDelete(DeleteBehavior.ClientCascade);
            modelBuilder.Entity<Order>()
                .HasOne(sd => sd.User)
                .WithMany(s => s.CurrentOrderList)
                .HasForeignKey(sd => sd.UserId)
                .OnDelete(DeleteBehavior.ClientCascade);
            
             
            /*
             * a product has many specsDescription and a specsDescription has one product
             */
            modelBuilder.Entity<Product>()
                .HasMany(p => p.CurrentOrderList)
                .WithOne(sd => sd.Product)
                .OnDelete(DeleteBehavior.ClientCascade);
            /*
            * a specs has many specsDescription and a specsDescription has one specs
            */
            modelBuilder.Entity<User>()
                .HasMany(s => s.CurrentOrderList)
                .WithOne(sd => sd.User)
                .OnDelete(DeleteBehavior.ClientCascade);
            
            
            
            
            
            // a main category has many products, but product has one main category 
            modelBuilder.Entity<MainCategory>()
                .HasMany<Product>(mc => mc.ProdList)
                .WithOne(p => p.MainCategoryObj);
            //  a sub category has many products, but product has one sub category
            modelBuilder.Entity<SubCategory>()
                .HasMany<Product>(sc => sc.ProdList)
                .WithOne(p => p.SubCategoryObj);
            // a brand has many products, but product has one brand 
            modelBuilder.Entity<Brand>()
                .HasMany<Product>(sc => sc.ProdList)
                .WithOne(p => p.Brand);
            //Making foreign keys for the categories. The product stores a single category but the categories store multiple products
            modelBuilder.Entity<Product>()
                .HasOne<MainCategory>(p => p.MainCategoryObj)
                .WithMany(c => c.ProdList)
                .HasForeignKey(p => p.MainCategoryID);
            modelBuilder.Entity<Product>()
                .HasOne<SubCategory>(p => p.SubCategoryObj)
                .WithMany(c => c.ProdList)
                .HasForeignKey(p => p.SubCategoryID);
            modelBuilder.Entity<Product>()
                .HasOne<Brand>(p => p.Brand)
                .WithMany(c => c.ProdList)
                .HasForeignKey(p => p.BrandID);

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
                .WithOne(sd => sd.Product)
                .OnDelete(DeleteBehavior.ClientCascade);
            /*
            * a specs has many specsDescription and a specsDescription has one specs
            */
            modelBuilder.Entity<Specs>()
                .HasMany(s => s.SpecsDescriptions)
                .WithOne(sd => sd.Specs)
                .OnDelete(DeleteBehavior.ClientCascade);
            

            //Dont auto include  
            modelBuilder.Entity<SpecsDescription>()
                .Ignore(sd => sd.Product);
            modelBuilder.Entity<SpecsDescription>()
                .Ignore(sd => sd.Specs);
            modelBuilder.Entity<Product>()
                .Ignore(p => p.MainCategoryObj);
            modelBuilder.Entity<Product>()
                .Ignore(p => p.SubCategoryObj);
            modelBuilder.Entity<Product>()
                .Ignore(p => p.Brand);
            modelBuilder.Entity<Product>()
                .Ignore(p => p.Ratings);
            modelBuilder.Entity<Specs>()
                .Ignore(s => s.SpecsDescriptions);
            modelBuilder.Entity<User>()
                .Ignore(u => u.Ratings);
            modelBuilder.Entity<MainCategory>()
                .Ignore(c => c.ProdList);
            modelBuilder.Entity<SubCategory>()
                .Ignore(c => c.ProdList);
            modelBuilder.Entity<Brand>()
                .Ignore(c => c.ProdList);
            modelBuilder.Entity<Order>()
                .Ignore(o => o.Product);
            modelBuilder.Entity<Order>()
                .Ignore(o => o.User);
            modelBuilder.Entity<Product>()
                .Ignore(p => p.CurrentOrderList);
        }
        
        public DbSet<SpecsDescription> SpecsDescriptionsTable { get; set; }
        public DbSet<Product> ProductTable { get; set; }
        public DbSet<MainCategory> MainCategoryTable { get; set; }
        public DbSet<SubCategory> SubCategoryTable { get; set; }
        public DbSet<Specs> SpecsTable { get; set; }
        public DbSet<Brand> BrandTable { get; set; }
        public DbSet<User> UserTable { get; set; }
        public DbSet<Rating> RatingsTable { get; set; }
        public DbSet<Order> OrdersTable { get; set; }
    }

}