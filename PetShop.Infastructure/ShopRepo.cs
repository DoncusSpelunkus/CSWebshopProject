using PetShop.Application.Interfaces;
using PetShop.Domain;

namespace PetShop.Infastructure
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
            Product product = GetProductByID(productID);
            _shopDbContext.ProductTable.Remove(product);
            _shopDbContext.SaveChanges();
            return product;
        }
        

        public Product GetProductByID(int productId)
        {
            return _shopDbContext.ProductTable.FirstOrDefault(p => p.ID == productId);
        }

        public void RebuildDB()
        {
            _shopDbContext.Database.EnsureDeleted();
            _shopDbContext.Database.EnsureCreated();
        }
    }
}
