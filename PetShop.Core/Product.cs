namespace Factory.Domain
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
        //public List<KeyValuePair<int,Specs>> Specs{ get; set; }
        public int MainCategory { get; set; }
        public int SubCategory { get; set; }
        public int Brand { get; set; }

    }
}