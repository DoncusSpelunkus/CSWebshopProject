using PetShop.Domain;

namespace PetShop.Application.Interfaces
{
    public interface IShopRepo
    {
        public List<Product> GetAllProducts();

        public Product CreateProduct(Product product);
        
        public Product UpdateProduct(Product product);

        public Product DeleteProduct(int productId);

        public Product GetProductById(int productId);
        
        public void RebuildDB();
        
        public Rating AddRating(Rating rating);
        public Rating UpdateRating(Rating rating);
        public List<Rating> GetAllRatings();
        public int GetProductId(int productId);
    }
}
