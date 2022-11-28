using Microsoft.EntityFrameworkCore;
using PetShop.Domain;

namespace PetShop.Infastructure;

public class SpecsDbContext : DbContext
{
    public SpecsDbContext(DbContextOptions<SpecsDbContext> opts) : base(opts)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Specs>()
            .Property(s => s.ID)
            .ValueGeneratedOnAdd();
    }

    public DbSet<Specs> SpecsTable { get; set; }
    
    

}