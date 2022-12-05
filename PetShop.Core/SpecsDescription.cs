using System.ComponentModel.DataAnnotations.Schema;

namespace PetShop.Domain;

public class SpecsDescription
{
    public  int ID { get; set; }
    public Product Product { get; set; }
    public int ProductId { get; set; }
    public Specs Specs { get; set; }
    public int SpecsId { get; set; }
    
    public string Description { get; set; }
}