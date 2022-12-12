using System.ComponentModel.DataAnnotations;

namespace PetShop.Application.PostProdDTO;

public class UserLoginDTO
{
    [Required]
    [StringLength(150)]
    [EmailAddress]
    public String Email { get; set; }
    [Required]
    [StringLength(20)]
    public String Password { get; set; }
}