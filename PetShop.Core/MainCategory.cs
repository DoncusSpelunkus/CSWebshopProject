using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using PetShop.Domain;

namespace Factory.Domain;

public class MainCategory
{

    [Key]
    public int MainCategoryID { get; set; }
    public string Name { get; set; }
    
    public List<Product>?  ProdList{ get; set; }

}