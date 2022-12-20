using PetShop.Domain;

namespace PetShop.Application.Interfaces;

public interface ISpecRepo
{
    public List<Specs> GetAllSpecs();
    public Specs CreateSpecs(Specs specs);
    public Specs UpdateSpecs(Specs specs);
    public Specs DeleteSpecsById(int id);
    public Specs GetSpecsById(int specsId);

}