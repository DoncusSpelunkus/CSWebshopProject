using PetShop.Domain;
using Microsoft.EntityFrameworkCore;
using PetShop.Application.Interfaces;

namespace PetShop.Infastructure;

public class BrandRepo : IBrandRepo
{
    private DBContext _dbContext;

    public BrandRepo(DBContext dbContext)
    {
        _dbContext = dbContext;
    }
    public List<Brand> GetAllBrands()
    {
        return _dbContext.BrandTable.ToList();
    }

    public Brand CreateBrand(Brand brand)
    {
        _dbContext.BrandTable.Add(brand);
        _dbContext.SaveChanges();
        return brand;
    }

    public Brand UpdateBrand(Brand brand)
    {
        _dbContext.BrandTable.Update(brand);
        _dbContext.SaveChanges();
        return brand;
    }

    public Brand DeleteBrand(int brandId)
    {
        Brand brand = GetBrandById(brandId);
        _dbContext.BrandTable.Remove(brand);
        _dbContext.SaveChanges();
        return brand;
    }

    public Brand GetBrandById(int brandId)
    {
        return _dbContext.BrandTable.FirstOrDefault(b => b.Id == brandId);
    }
}