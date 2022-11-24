using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Factory.Application.Interfaces;
using Factory.Domain;

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
            return _shopDbContext.BoxTable.ToList();
        }

        public Product CreateProduct(Product product)
        {
            _shopDbContext.BoxTable.Add(product);
            _shopDbContext.SaveChanges();
            return product;
        }

        public Product UpdateProduct(Product product)
        {
            _shopDbContext.BoxTable.Update(product);
            _shopDbContext.SaveChanges();
            return product;
        }
        
        public Product DeleteProduct(int productID)
        {
            Product product = GetProductByID(productID);
            _shopDbContext.BoxTable.Remove(product);
            _shopDbContext.SaveChanges();
            return product;
        }
        

        public Product GetProductByID(int productId)
        {
            return _shopDbContext.BoxTable.FirstOrDefault(p => p.ID == productId);
        }

        public void CreateDB()
        {
            _shopDbContext.Database.EnsureDeleted();
            _shopDbContext.Database.EnsureCreated();
        }
    }
}
