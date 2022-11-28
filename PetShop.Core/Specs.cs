using System.ComponentModel.DataAnnotations;

namespace PetShop.Domain;

public class Specs
{
    public int ID { get; set; }
    [MaxLength(256)]
    public string SpecName { get; set; }    

}