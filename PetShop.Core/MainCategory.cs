using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using PetShop.Domain;

namespace Factory.Domain;

public class MainCategory
{

    [Key]
    public int MainCategoryID { get; set; }
    public string Name { get; set; }
    [AllowNull]
    public virtual ICollection<Product> ProdList { get; set; }

}