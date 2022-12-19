using PetShop.Domain;
using PetShop.Application.PostProdDTO;

namespace PetShop.Application.Interfaces;

public interface ICatRepo
{
    public List<MainCategory> GetAllMainCategories();
    public MainCategory CreateMainCategory(MainCategory mainCategory);
    public MainCategory UpdateMainCategory(MainCategory mainCategory);
    public MainCategory GetMainCategoryByID(int mainCatId);
    public MainCategory DeleteMainCategoryByID(int mainCatId);
    public List<SubCategory> GetAllSubCategories();
    public SubCategory CreateSubCategory(SubCategory subCategory);
    public SubCategory UpdateSubCategory(SubCategory subCategory);
    public SubCategory GetSubCategoryByID(int subCatId);
    public SubCategory DeleteSubCategoryByID(int subCatId);
}