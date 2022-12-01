using System.ComponentModel.DataAnnotations.Schema;

namespace PetShop.Domain;

public class SpecsDescription
{
    [ForeignKey("CreatedByProductID")]
    public int FromProductID { get; set; }
    
    [ForeignKey("CreatedBySpecsID")]
    public int FromSpecId { get; set; }
    
    public string Description { get; set; }
}