using Factory.Domain;
using PetShop.Domain;

namespace PetShop.Application.Interfaces
{
    public interface IShopRepo
    {
        public List<Product> GetAllProducts();

        public Product CreateProduct(Product product);
        
        public Product UpdateProduct(Product product);

        public Product DeleteProduct(int productID);

        public Product GetProductByID(int productID);

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


        public void RebuildDB();
    }
}
