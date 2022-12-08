using System.ComponentModel.DataAnnotations;

namespace PetShop.Application.PostProdDTO;

public class UserDTO
{   
    [Required]
    public string Name { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [StringLength(20)]
    public string password { get; set; }
    public int type { get; set; }
}