using PetShop.Application.PostProdDTO;
using PetShop.Domain;

namespace PetShop.Application.Interfaces;

public interface ISpecService
{
    public List<Specs> GetAllSpecs();
        
    public Specs CreateSpecs(SpecDTO dto);

    public Specs UpdateSpecs(int specId, SpecDTO dto);
        
    public Specs DeleteSpecsById(int SpecId);

    public Specs GetSpecById(int SpecId);
}