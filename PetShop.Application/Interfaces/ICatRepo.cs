using PetShop.Domain;
using PetShop.Application.PostProdDTO;

namespace PetShop.Application.Interfaces;

public interface ICatRepo
{
    public List<MainCategory> GetAllMainCategories();
    public MainCategory CreateMainCategory(MainCategory mainCategory);
    public MainCategory UpdateMainCategory(MainCategory mainCategory);
    public MainCategory GetMainCategoryById(int mainCatId);
    public MainCategory DeleteMainCategoryById(int mainCatId);
    public List<SubCategory> GetAllSubCategories();
    public SubCategory CreateSubCategory(SubCategory subCategory);
    public SubCategory UpdateSubCategory(SubCategory subCategory);
    public SubCategory GetSubCategoryById(int subCatId);
    public SubCategory DeleteSubCategoryById(int subCatId);
}