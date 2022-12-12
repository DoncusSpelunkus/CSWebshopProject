namespace PetShop.Application.PostProdDTO

{
    public class ProdDTO
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        public List<SpecDescDTO> SpecsDescriptions { get; set; }
        public int MainCategoryID { get; set; }
        public int SubCategoryID { get; set; }        
        public int BrandID { get; set; }

    }
    
    public class ratingDTO
    {
        public int RatingValue { get; set; }
    }
}
