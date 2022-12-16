using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using PetShop.Domain;

namespace PetShop.Domain;

public class SubCategory
{
    [Key]
    public int SubCategoryID { get; set; }
    public string Name { get; set; }
    public List<Product>?  ProdList{ get; set; }
}