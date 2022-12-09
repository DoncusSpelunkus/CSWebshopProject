using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using PetShop.Domain;

namespace Factory.Domain;

public class SubCategory
{
    [Key]
    public int SubCategoryID { get; set; }
    public string Name { get; set; }
    [AllowNull]
    public ICollection<Product> ProdList { get; set; }
}