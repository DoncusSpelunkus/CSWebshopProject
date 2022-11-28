using PetShop.Application.Interfaces;
using PetShop.Domain;

namespace PetShop.Infastructure;

public class SpecsRepo : ISpecRepo

{
    private SpecsDbContext _specsDbContext;

    public SpecsRepo(SpecsDbContext specsDbContext)
    {
        _specsDbContext = specsDbContext;
    }
    
    
    public List<Specs> GetAllSpecs()
    {
        return _specsDbContext.SpecsTable.ToList();
    }

    public Specs CreateSpecs(Specs specs)
    {
        _specsDbContext.SpecsTable.Add(specs);
        _specsDbContext.SaveChanges();
        return specs;
    }

    public Specs UpdateSpecs(Specs specs)
    {
        _specsDbContext.SpecsTable.Update(specs);
        _specsDbContext.SaveChanges();
        return specs;
    }

    public Specs DeleteSpecsById(int specsId)
    {
        Specs specs = GetSpecsByID(specsId);
        _specsDbContext.SpecsTable.Remove(specs);
        _specsDbContext.SaveChanges();
        return specs;
    }

    public Specs GetSpecsByID(int specsId)
    {
       
           return _specsDbContext.SpecsTable.FirstOrDefault(s => s.ID == specsId);

       
    }
}