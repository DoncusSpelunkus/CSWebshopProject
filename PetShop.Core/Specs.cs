using System.ComponentModel.DataAnnotations;

namespace PetShop.Domain;

public class Specs
{
    [Key]
    public int ID { get; set; }
    public string SpecName { get; set; }

    public List<SpecsDescription>? SpecsDescriptions { get; set; }

}