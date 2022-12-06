using System.ComponentModel.DataAnnotations;
using Factory.Domain;

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

        //public List<KeyValuePair<string,string>> Specs{ get; set; }
        public MainCategory MainCategoryObj { get; set; }

        public int MainCategoryObjId { get; set; }
        public SubCategory SubCategoryObj { get; set; }
        public int SubCategoryObjId { get; set; }
        
        /*
         *The specslist holds a list of names of the specs.
         * 
         */
        //private List<Specs> _specsList { get; set; }
        public List<SpecsDescription> SpecsDescriptions { get; set; }
        public int Brand { get; set; }
        

    }
}