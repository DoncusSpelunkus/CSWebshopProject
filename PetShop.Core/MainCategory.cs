using PetShop.Domain;

namespace Factory.Domain;

public class MainCategory
{
    public int RefID { get; set; }
    
    public string Name { get; set; }
    public List<Product> ProdList { get; set; }

}