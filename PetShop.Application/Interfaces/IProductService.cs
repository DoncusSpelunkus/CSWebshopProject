using PetShop.Application.PostProdDTO;
using PetShop.Domain;

namespace PetShop.Application.Interfaces
{
    public interface IProductService
    {
        public List<Product> GetAllProducts();
        
        public Product CreateProduct(ProdDTO dto);

        public Product UpdateProduct(int productId, ProdDTO dto);
        
        public Product DeleteProduct(int productId);

        public Product GetProductById(int productId);
        
        public void RebuildDB();
        public Rating AddRating(ratingDTO rating, int productId, Guid userId);
        public Rating UpdateRating(ratingDTO ratingDto, int productId, Guid userId);
    }
}

