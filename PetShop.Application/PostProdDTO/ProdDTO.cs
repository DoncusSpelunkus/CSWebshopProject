using System.ComponentModel.DataAnnotations;
using PetShop.Domain;

namespace PetShop.Application.PostProdDTO
{
    public class ProdDTO
    {
        [MaxLength(256)]
        public string Name { get; set; }
        [MaxLength(256)]
        public double Price { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public double Rating { get; set; }
        
        public List<SpecDescDTO> SpecsDescriptions { get; set; }
        public int MainCategory { get; set; }
        public int SubCategory { get; set; }        
        public int Brand { get; set; }

    }
}
