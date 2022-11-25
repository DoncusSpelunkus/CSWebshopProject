using PetShop.Domain;

namespace PetShop.Domain
{
    public class Product
    {
        // push
        public int ID { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public double Rating { get; set; }
        
        /*
         *The list should hold the Specs entity which it self holds on to an id, specs and a description of the specs
         * and an int, which is the ID.
         * 
         */
        private List<Specs> _specsList { get; set; }
        public int MainCategory { get; set; }
        public int SubCategory { get; set; }
        public int Brand { get; set; }

    }
}