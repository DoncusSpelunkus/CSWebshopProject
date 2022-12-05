using PetShop.Domain;

namespace Factory.Domain;

public class Brand
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Product ProdList { get; set; }
}