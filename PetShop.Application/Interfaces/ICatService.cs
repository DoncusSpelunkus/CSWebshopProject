using PetShop.Domain;
using PetShop.Application.PostProdDTO;

namespace PetShop.Application.Interfaces;

public interface ICatService
{
    public List<MainCategory> GetAllMainCategories();
    public MainCategory CreateMainCategory(MainCatDTO mainCategory);
    public MainCategory UpdateMainCategory(int mainCatRefId, MainCatDTO mainCategory);
    public MainCategory GetMainCategoryById(int mainCatId);
    public MainCategory DeleteMainCategoryById(int mainCatId);
    public List<SubCategory> GetAllSubCategories();
    public SubCategory CreateSubCategory(SubCatDTO subCategory);
    public SubCategory UpdateSubCategory(int subCatRefId, SubCatDTO subCategory);
    public SubCategory GetSubCategoryById(int subCatId);
    public SubCategory DeleteSubCategoryById(int subCatId);
}