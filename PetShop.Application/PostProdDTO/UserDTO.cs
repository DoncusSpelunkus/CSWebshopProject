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
    public string Address { get; set; }
    public int Zipcode { get; set; }
    public string City { get; set; }
    public int PhoneNumber { get; set; }
}