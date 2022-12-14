using PetShop.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

using PetShop.Domain;

namespace PetShop.Infastructure
{
    public class ShopRepo : IShopRepo
    {
        private DBContext _dbContext;
        private IShopRepo _shopRepoImplementation;

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
        public Rating AddRating(Rating rating)
        {
            _dbContext.RatingsTable.Add(rating);
            _dbContext.SaveChanges();
            return rating;
        }
       

        
        public Rating UpdateRating(Rating rating)
        {
            var existingRating =
                _dbContext.RatingsTable.FirstOrDefaultAsync(r =>
                    r.ProductId == rating.ProductId && r.UserId == rating.UserId).Result;
            if (existingRating != null)
            {
                _dbContext.RatingsTable.Update(existingRating);
                _dbContext.SaveChanges();
                
            }

            return rating;

        }
        
        
        // method for getting the average rating of a product
        public double GetTheAverageRatingForProduct(int productId)
        {   
            // get all the ratings for the specific product
            List<int> ratings = _dbContext.RatingsTable.Where(r => r.ProductId == productId).Select(r => r.RatingValue).ToList();
            var count = 0;
            var sum = 0;

            for (int i = 0; i < ratings.Count; i++)
            {
                count++;
                sum += ratings[i];
            }

            double average = sum / count;

            return average;
        }
      

        public int GetProductID(int productId)
        {
            return _shopRepoImplementation.GetProductID(productId);
        }
        
        
        public void RebuildDB()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Database.EnsureCreated();
        }
    }
}
