using Factory.Domain;
using PetShop.Application.PostProdDTO;

namespace PetShop.Application.Interfaces;

public interface IBrandService
{
    public List<Brand> GetAllBrands();
        
    public Brand CreateBrand(BrandDto dto);

    public Brand UpdateBrand(int brandID, BrandDto brandDto);
        
    public Brand DeleteBrand(int brandID);

    public Brand GetBrandByID(int brandID);
}