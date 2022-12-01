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
            //Auto increment
            modelBuilder.Entity<Product>()
                .Property(p => p.ID)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<MainCategory>()
                .Property(f => f.RefID)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<SubCategory>()
                .Property(f => f.RefID)
                .ValueGeneratedOnAdd();

            //Setting keys
            modelBuilder.Entity<Product>()
                .HasKey(c => new { ManFacId = c.ID });
            modelBuilder.Entity<MainCategory>()
                .HasKey(c => new { ManFacId = c.RefID });
            modelBuilder.Entity<SubCategory>()
                .HasKey(c => new { ManFacId = c.RefID });
            
            //Making foreign keys for the categories. The product stores a single category but the categories store multiple products
            modelBuilder.Entity<Product>()
                .HasOne<MainCategory>(p => p.MainCategoryObj)
                .WithMany(c => c.ProdList)
                .HasForeignKey(p => p.MainCategoryObj);
            modelBuilder.Entity<Product>()
                .HasOne<SubCategory>(p => p.SubCategoryObj)
                .WithMany(c =>c.ProdList)
                .HasForeignKey(p=> p.SubCategoryObj);
            modelBuilder.Entity<Product>()
                .Ignore(p => p.MainCategoryObj);
        }

        public DbSet<Product> ProductTable { get; set; }
        public DbSet<MainCategory> MainCategoryTable { get; set; }
        public DbSet<SubCategory> SubCategoryTable { get; set; }
    }
}