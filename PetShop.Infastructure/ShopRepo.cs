using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using Factory.Application.Interfaces;
using Factory.Domain;
using Microsoft.EntityFrameworkCore;

namespace Factory.Infastructure
{
    public class ShopRepo : IShopRepo
    {
        private ShopDbContext _shopDbContext;

        public ShopRepo(ShopDbContext shopDbContext)
        {
            _shopDbContext = shopDbContext;
        }

        public List<Product> GetAllProducts()
        {
            return _shopDbContext.ProductTable.ToList();
        }

        public Product CreateProduct(Product product)
        {
            _shopDbContext.ProductTable.Add(product);
            _shopDbContext.SaveChanges();
            return product;
        }

        public Product UpdateProduct(Product product)
        {
            _shopDbContext.ProductTable.Update(product);
            _shopDbContext.SaveChanges();
            return product;
        }
        
        public Product DeleteProduct(int productID)
        {
            var product = GetProductByID(productID);
            _shopDbContext.ProductTable.Remove(product);
            _shopDbContext.SaveChanges();
            return product;
        }
        

        public Product GetProductByID(int productId)
        {
            return _shopDbContext.ProductTable.FirstOrDefault(p => p.ID == productId);
        }

        public List<MainCategory> GetAllMainCategories()
        {
            return _shopDbContext.MainCategoryTable.Include(c=> c.ProdList).ToList();
        }
        
        public MainCategory CreateMainCategory(MainCategory mainCategory)
        {
            _shopDbContext.MainCategoryTable.Add(mainCategory);
            _shopDbContext.SaveChanges();
            return mainCategory;
        }

        public MainCategory UpdateMainCategory(MainCategory mainCategory)
        {
            _shopDbContext.MainCategoryTable.Update(mainCategory);
            _shopDbContext.SaveChanges();
            return mainCategory;
        }
        public MainCategory GetMainCategoryByID(int mainCatId)
        {
            return _shopDbContext.MainCategoryTable.FirstOrDefault(c => c.RefID == mainCatId);
        }

        public MainCategory DeleteMainCategoryByID(int mainCatId)
        {
            MainCategory mainCategory = GetMainCategoryByID(mainCatId);
            _shopDbContext.MainCategoryTable.Remove(mainCategory);
            _shopDbContext.SaveChanges();
            return mainCategory;
        }

        public List<SubCategory> GetAllSubCategories()
        {
            return _shopDbContext.SubCategoryTable.ToList();
        }

        public SubCategory CreateSubCategory(SubCategory subCategory)
        {
            _shopDbContext.MainCategoryTable.Add(subCategory);
            _shopDbContext.SaveChanges();
            return subCategory;
        }
        
        public SubCategory UpdateSubCategory(SubCategory subCategory)
        {
            _shopDbContext.MainCategoryTable.Update(subCategory);
            _shopDbContext.SaveChanges();
            return subCategory;
        }
        
        public SubCategory GetSubCategoryByID(int subCatId)
        {
            return _shopDbContext.SubCategoryTable.FirstOrDefault(c => c.RefID == subCatId);
        }

        public SubCategory DeleteSubCategoryByID(int subCatId)
        {
            SubCategory subCategory = GetSubCategoryByID(subCatId);
            _shopDbContext.MainCategoryTable.Remove(subCategory);
            _shopDbContext.SaveChanges();
            return subCategory;
        }

        public void RebuildDB()
        {
            _shopDbContext.Database.EnsureDeleted();
            _shopDbContext.Database.EnsureCreated();
        }
    }
}
