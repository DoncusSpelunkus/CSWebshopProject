using System.ComponentModel.DataAnnotations;
using System.Reflection;
using PetShop.Domain;

namespace PetShop.Domain
{
    public class Product
    {
        [Key]
        public int ID { get; set; }
        [MaxLength(256)]
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public double Rating { get; set; }
        public MainCategory? MainCategoryObj { get; set; }
        public int MainCategoryObjId { get; set; }
        public SubCategory? SubCategoryObj { get; set; }
        public int SubCategoryObjId { get; set; }
        public List<SpecsDescription> SpecsDescriptions { get; set; }
        public Brand? Brand { get; set; }
        public List<Rating> Ratings { get; set; }
       
        public int BrandID { get; set; }


    }
    public class Rating
    {  
        public int RatingValue { get; set; } // The rating value, from 1 to 5
        public int ProductId { get; set; } // The ID of the product being rated
        public Guid UserId { get; set; } // The ID of the user who posted the rating

        // Navigation properties
        public  Product Product { get; set; }
        public  User User { get; set; }
    }
}