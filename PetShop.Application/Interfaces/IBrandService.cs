using Factory.Domain;
using PetShop.Application.PostProdDTO;

namespace PetShop.Application.Interfaces;

public interface IBrandService
{
    public List<Brand> GetAllBrands();
        
    public Brand CreateBrand(ProdDTO dto);

    public Brand UpdateBrand(int brandID, Brand brand);
        
    public Brand DeleteBrand(int brandID);

    public Brand GetBrandByID(int brandID);
}