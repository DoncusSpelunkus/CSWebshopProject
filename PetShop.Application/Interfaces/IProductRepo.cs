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
        
        public void RebuildDB();
        
        public void AddRating(int ratingValue, int productId, Guid userId);
        public void UpdateRating(int ratingValue, int productId, Guid userId);
        public int GetRating(int productId, Guid userId);
    }
}
