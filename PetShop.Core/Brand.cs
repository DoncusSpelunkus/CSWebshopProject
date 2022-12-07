using PetShop.Domain;

namespace PetShop.Domain;

public class Brand
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Product ProdList { get; set; }
}