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
        
        public Rating AddRating(Rating rating);
        public Rating UpdateRating(int ratingValue, int productId, Guid userId);
        public int GetTheAverageRatingForProduct(int productId);
        public int GetProductID(int productId);
    }
}
