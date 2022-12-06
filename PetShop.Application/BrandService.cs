using Factory.Domain;
using PetShop.Application.Interfaces;
using PetShop.Application.PostProdDTO;

namespace PetShop.Application;

public class BrandService: IBrandService
{
    public List<Brand> GetAllBrands()
    {
        throw new NotImplementedException();
    }

    public Brand CreateBrand(ProdDTO dto)
    {
        throw new NotImplementedException();
    }

    public Brand UpdateBrand(int brandID, Brand brand)
    {
        throw new NotImplementedException();
    }

    public Brand DeleteBrand(int brandID)
    {
        throw new NotImplementedException();
    }

    public Brand GetBrandByID(int brandID)
    {
        throw new NotImplementedException();
    }
}