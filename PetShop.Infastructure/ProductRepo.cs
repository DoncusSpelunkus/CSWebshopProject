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
            {List<int> ratings = _dbContext.RatingsTable.Where(r => r.ProductId == product.ID).Select(r => r.RatingValue).ToList();
                double count = 0;
                double sum = 0;
                foreach (var rating in ratings)
                {
                    count++;
                    sum = sum + rating;
                }

                double average;
                if (count != 0)
                { average = sum / count;
                    average = Math.Round(average, 2);
                    product.AverageRating = average;
                }
                
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
            var listOfProductSpecsDesc = _dbContext.SpecsDescriptionsTable.AsNoTracking().ToList();
            foreach (var specs in listOfProductSpecsDesc)
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
        
        public Product DeleteProduct(int productId)
        {
            Product product = GetProductById(productId);
            _dbContext.ProductTable.Remove(product);
            _dbContext.SaveChanges();
            return product;
        }
        

        public Product GetProductById(int productId)
        {
            Product product = new Product();
            var listOfProductsSpecsDescriptions = new List<SpecsDescription>();
            var specsDescription = _dbContext.SpecsDescriptionsTable.ToList();
            List<int> ratings = _dbContext.RatingsTable.Where(r => r.ProductId == productId).Select(r => r.RatingValue).ToList();
            double count = 0;
            double sum = 0;


            foreach (var rating in ratings)
            {
                count++;
                sum = sum + rating;
            }
            
            foreach (var specs in specsDescription)
            {
                if (productId == specs.ProductId) 
                {
                 listOfProductsSpecsDescriptions.Add(specs);   
                }
                
            }
            
            product =  _dbContext.ProductTable.FirstOrDefault(p => p.ID == productId);
            product.SpecsDescriptions = listOfProductsSpecsDescriptions;
            double average;
            if (count != 0)
            { average = sum / count;
                average = Math.Round(average, 2);
                product.AverageRating = average;
            }
           
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
            var existingRating = _dbContext.RatingsTable.FirstOrDefaultAsync(r =>
                    r.ProductId == rating.ProductId && r.UserId == rating.UserId).Result;
            existingRating.RatingValue = rating.RatingValue;
            if (existingRating != null)
            {
                _dbContext.RatingsTable.Update(existingRating);
                _dbContext.SaveChanges();
                
            }

            return rating;

        }

        public int GetProductId(int productId)
        {
            return _shopRepoImplementation.GetProductId(productId);
        }
        public List<Rating> GetAllRatings()
        {
            return _dbContext.RatingsTable.ToList();
        }
        
        public void RebuildDB()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Database.EnsureCreated();
        }
    }
}
