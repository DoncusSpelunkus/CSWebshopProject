using PetShop.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

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
            var specsList = _dbContext.SpecsDescriptionsTable.ToList();
            var producttablelist= _dbContext.ProductTable.ToList();
            
            foreach (var product in producttablelist)
            {
                var listofSpecDesc = new List<SpecsDescription>();
                foreach (var specDesc in specsList)
                {
                    
                    if (product.ID == specDesc.ProductId)
                    {
                        listofSpecDesc.Add(specDesc);
                    }
                    
                }
                product.SpecsDescriptions = listofSpecDesc;
            }

            return producttablelist;
        }

        public Product CreateProduct(Product product)
        {

            foreach (var productSpecsDescription in product.SpecsDescriptions)
            {
                productSpecsDescription.ProductId = product.ID;
                _dbContext.SpecsDescriptionsTable.Add(productSpecsDescription);
            }
            _dbContext.ProductTable.Add(product);
            _dbContext.SaveChanges();
            return product;
        }

        public Product UpdateProduct(Product product)
        {
            var listofProductspecs = _dbContext.SpecsDescriptionsTable.AsNoTracking().ToList();
            foreach (var specs in listofProductspecs)
            {
                if (specs.ProductId == product.ID)
                {
                    _dbContext.SpecsDescriptionsTable.Remove(specs);
                }
            }

            foreach (var productSpecsDescription in product.SpecsDescriptions)
            {
                productSpecsDescription.ProductId = product.ID;
                _dbContext.SpecsDescriptionsTable.Add(productSpecsDescription);
            }
            product.SpecsDescriptions.Clear();
            _dbContext.ProductTable.Update(product);
            _dbContext.SaveChanges();
            return product;
        }
        
        public Product DeleteProduct(int productID)
        {
            Product product = GetProductByID(productID);
            foreach (var productSpecsDescription in product.SpecsDescriptions)
            {
                if (product.ID == productSpecsDescription.ProductId)
                {
                    _dbContext.SpecsDescriptionsTable.Remove(productSpecsDescription);
                }
              
            }
            _dbContext.ProductTable.Remove(product);
            _dbContext.SaveChanges();
            return product;
        }
        

        public Product GetProductByID(int productId)
        {
            Product product = new Product();
            var ListofProductsSpecsDescriptions = new List<SpecsDescription>();
            var SpecsDiscription = _dbContext.SpecsDescriptionsTable.ToList();
            foreach (var specs in SpecsDiscription)
            {
                if (productId == specs.ProductId) 
                {
                 ListofProductsSpecsDescriptions.Add(specs);   
                }
                
            }
            
            product =  _dbContext.ProductTable.FirstOrDefault(p => p.ID == productId);
            product.SpecsDescriptions = ListofProductsSpecsDescriptions;
            return product;
        }
        
        public void RebuildDB()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Database.EnsureCreated();
            

        }
        public void AddRating(int ratingValue, int productId, Guid userId)
        {
            var rating = new Rating
            {
                RatingValue = ratingValue,
                ProductId = productId,
                UserId = userId
            };

            _dbContext.RatingsTable.Add(rating);
            _dbContext.SaveChanges();
        }

        public void UpdateRating(int ratingValue, int productId, Guid userId)
        {
            throw new NotImplementedException();
        }

        public int GetRating(int productId, Guid userId)
        {
            throw new NotImplementedException();
        }
        
        
    }
}
