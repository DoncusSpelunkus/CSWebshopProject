using System.Diagnostics.CodeAnalysis;
using PetShop.Domain;

namespace Factory.Domain;

public class MainCategory : ICategory
{
    public int RefID { get; set; }
    public string Name { get; set; }
    [AllowNull]
    public List<Product> ProdList { get; set; }

}