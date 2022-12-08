using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Factory.Domain;
using PetShop.Domain;

namespace PetShop.Application.PostProdDTO
{
    public class ProdDTO
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public double Rating { get; set; }
        
       // public List<SpecDescDTO> SpecsDescriptions { get; set; }
        [AllowNull]
        public int MainCategoryID { get; set; }
        [AllowNull] 
        public int SubCategoryID { get; set; }
        [AllowNull] 
        public int Brand { get; set; }
        

    }
}
