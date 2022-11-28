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
        //public List<KeyValuePair<string,string>> Specs{ get; set; }
        public MainCategory MainCategoryObj { get; set; }
        public SubCategory SubCategoryObj { get; set; }
        public int Brand { get; set; }

    }
}