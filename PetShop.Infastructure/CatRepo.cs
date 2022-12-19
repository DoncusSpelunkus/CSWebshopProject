using PetShop.Domain;
using Microsoft.EntityFrameworkCore;
using PetShop.Application.Interfaces;
using PetShop.Application.PostProdDTO;

namespace PetShop.Infastructure;

public class CatRepo: ICatRepo
{
    private DBContext _dbContext;

    public CatRepo(DBContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public MainCategory CreateMainCategory(MainCategory mainCategory)
    {
        _dbContext.MainCategoryTable.Add(mainCategory);
        _dbContext.SaveChanges();
        return mainCategory;
    }

    public List<MainCategory?> GetAllMainCategories()
    {
        return _dbContext.MainCategoryTable.ToList();
    }
    
    public MainCategory UpdateMainCategory(MainCategory mainCategory)
    {
        _dbContext.MainCategoryTable.Update(mainCategory);
        _dbContext.SaveChanges();
        return mainCategory;
    }
    public MainCategory GetMainCategoryByID(int mainCatId)
    {
        return _dbContext.MainCategoryTable.FirstOrDefault(c => c.MainCategoryID == mainCatId);
    }

    public MainCategory DeleteMainCategoryByID(int mainCatId)
    {
        MainCategory mainCategory = GetMainCategoryByID(mainCatId);
        _dbContext.MainCategoryTable.Remove(mainCategory);
        _dbContext.SaveChanges();
        return mainCategory;
    }

    public List<SubCategory> GetAllSubCategories()
    {
        return _dbContext.SubCategoryTable.ToList();
    }

    public SubCategory CreateSubCategory(SubCategory subCategory)
    {
        _dbContext.SubCategoryTable.Add(subCategory);
        _dbContext.SaveChanges();
        return subCategory;
    }
    
    public SubCategory UpdateSubCategory(SubCategory subCategory)
    {
        _dbContext.SubCategoryTable.Update(subCategory);
        _dbContext.SaveChanges();
        return subCategory;
    }
    
    public SubCategory GetSubCategoryByID(int subCatId)
    {
        return _dbContext.SubCategoryTable.FirstOrDefault(c => c.SubCategoryID == subCatId);
    }

    public SubCategory DeleteSubCategoryByID(int subCatId)
    {
        SubCategory subCategory = GetSubCategoryByID(subCatId);
        _dbContext.SubCategoryTable.Remove(subCategory);
        _dbContext.SaveChanges();
        return subCategory;
    }
}