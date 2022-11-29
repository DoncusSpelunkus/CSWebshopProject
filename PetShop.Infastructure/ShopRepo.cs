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

        public void RebuildDB()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Database.EnsureCreated();
            

        }
    }
}
