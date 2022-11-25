using Factory.Domain;

namespace Factory.Application.Interfaces
{
    public interface IShopRepo
    {
        public List<Product> GetAllProducts();

        public Product CreateProduct(Product product);
        
        public Product UpdateProduct(Product product);

        public Product DeleteProduct(int productID);

        public Product GetProductByID(int productID);
        
        public void RebuildDB();
    }
}
