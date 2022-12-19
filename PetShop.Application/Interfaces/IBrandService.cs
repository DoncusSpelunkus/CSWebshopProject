using PetShop.Domain;
using PetShop.Application.PostProdDTO;

namespace PetShop.Application.Interfaces;

public interface IBrandService
{
    public List<Brand> GetAllBrands();
        
    public Brand CreateBrand(BrandDto dto);

    public Brand UpdateBrand(int brandId, BrandDto brandDto);
        
    public Brand DeleteBrand(int brandId);

    public Brand GetBrandById(int brandId);
}