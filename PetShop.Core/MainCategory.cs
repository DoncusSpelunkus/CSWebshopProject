using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using PetShop.Domain;

namespace PetShop.Domain;

public class MainCategory
{
    [Key]
    public int RefID { get; set; }
    public string Name { get; set; }
    [AllowNull]
    public List<Product> ProdList { get; set; }

}