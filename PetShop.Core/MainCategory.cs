namespace PetShop.Domain;

public class MainCategory
{

    
    public int MainCategoryID { get; set; }
    public string Name { get; set; }
    
    public List<Product>?  ProdList{ get; set; }

}