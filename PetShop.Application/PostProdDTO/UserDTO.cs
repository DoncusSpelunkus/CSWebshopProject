using System.ComponentModel.DataAnnotations;

namespace PetShop.Application.PostProdDTO;

public class UserDTO
{   
    [Required]
    public string Name { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    [StringLength(20)]
    public string password { get; set; }
    public int type { get; set; }
    [Required]
    public string City { get; set; }
    [Required]
    public string Address { get; set; }
    [Required]
    public int Zip { get; set; }
    [Required]
    public int Phone { get; set; }
    
}