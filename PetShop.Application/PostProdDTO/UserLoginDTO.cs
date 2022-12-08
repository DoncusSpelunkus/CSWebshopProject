using System.ComponentModel.DataAnnotations;

namespace PetShop.Application.PostProdDTO;

public class UserLoginDTO
{
    [Required]
    [StringLength(150)]
    public String UserName { get; set; }
    [Required]
    [StringLength(20)]
    public String Password { get; set; }
}