using Factory.Domain;

namespace PetShop.Application.Interfaces;

public interface IBrandRepo
{
    public List<Brand> GetAllBrands();
    public Brand CreateBrand(Brand brand);
    public Brand UpdateBrand(Brand brand);
    public Brand DeleteBrand(int brandID);
    public Brand GetBrandByID(int brandID);

}