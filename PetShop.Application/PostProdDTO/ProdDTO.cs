using System.ComponentModel.DataAnnotations;
using PetShop.Domain;

namespace PetShop.Application.PostProdDTO
{
    public class ProdDTO
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        public List<SpecDescDTO> SpecsDescriptions { get; set; }
        public int MainCategoryObjId { get; set; }
        public int SubCategoryObjId { get; set; }        
        public int BrandID { get; set; }
        

    }
    
    public class ratingDTO
    {
        public int Rating { get; set; }
    }
}
