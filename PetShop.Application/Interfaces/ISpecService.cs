using PetShop.Application.PostProdDTO;
using PetShop.Domain;

namespace PetShop.Application.Interfaces;

public interface ISpecService
{
    public List<Specs> GetAllSpecs();
        
    public Specs CreateSpecs(SpecDTO dto);

    public Specs UpdateSpecs(int SpecID, SpecDTO dto);
        
    public Specs DeleteSpecsById(int SpecID);

    public Specs GetSpecByID(int SpecID);
}