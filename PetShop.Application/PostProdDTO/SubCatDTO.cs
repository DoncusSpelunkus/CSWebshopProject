using System.Diagnostics.CodeAnalysis;
using PetShop.Domain;

namespace PetShop.Application.PostProdDTO;

public class SubCatDTO
{
    public string Name { get; set; }
    [AllowNull]
    public List<Product> ProdList { get; set; }
}