
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using PetShop.Application.Interfaces;
using Factory.Domain;
using Microsoft.EntityFrameworkCore;
 using PetShop.Application.Interfaces;
using PetShop.Domain;

namespace PetShop.Infastructure
{
    public class ShopRepo : IShopRepo
    {
        private DBContext _dbContext;

        public ShopRepo(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Product> GetAllProducts()
        {
            return _dbContext.ProductTable.ToList();
        }

        public Product CreateProduct(Product product)
        {
            _dbContext.ProductTable.Add(product);
            _dbContext.SaveChanges();
            return product;
        }

        public Product UpdateProduct(Product product)
        {
            _dbContext.ProductTable.Update(product);
            _dbContext.SaveChanges();
            return product;
        }
        
        public Product DeleteProduct(int productID)
        {
            Product product = GetProductByID(productID);
            _dbContext.ProductTable.Remove(product);
            _dbContext.SaveChanges();
            return product;
        }
        

        public Product GetProductByID(int productId)
        {
            return _dbContext.ProductTable.FirstOrDefault(p => p.ID == productId);
        }

        public List<MainCategory> GetAllMainCategories()
        {
            return _dbContext.MainCategoryTable.Include(c=> c.ProdList).ToList();
        }
        
        public MainCategory CreateMainCategory(MainCategory mainCategory)
        {
            _dbContext.MainCategoryTable.Add(mainCategory);
            _dbContext.SaveChanges();
            return mainCategory;
        }

        public MainCategory UpdateMainCategory(MainCategory mainCategory)
        {
            _dbContext.MainCategoryTable.Update(mainCategory);
            _dbContext.SaveChanges();
            return mainCategory;
        }
        public MainCategory GetMainCategoryByID(int mainCatId)
        {
            return _dbContext.MainCategoryTable.FirstOrDefault(c => c.RefID == mainCatId);
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
            _dbContext.MainCategoryTable.Add(subCategory);
            _dbContext.SaveChanges();
            return subCategory;
        }
        
        public SubCategory UpdateSubCategory(SubCategory subCategory)
        {
            _dbContext.MainCategoryTable.Update(subCategory);
            _dbContext.SaveChanges();
            return subCategory;
        }
        
        public SubCategory GetSubCategoryByID(int subCatId)
        {
            return _dbContext.SubCategoryTable.FirstOrDefault(c => c.RefID == subCatId);
        }

        public SubCategory DeleteSubCategoryByID(int subCatId)
        {
            SubCategory subCategory = GetSubCategoryByID(subCatId);
            _dbContext.MainCategoryTable.Remove(subCategory);
            _dbContext.SaveChanges();
            return subCategory;
        }

        public void RebuildDB()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Database.EnsureCreated();
            

        }
    }
}
