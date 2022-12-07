namespace PetShop.Application.PostProdDTO;

public class UserDTO
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string passwordSalt { get; set; }
    public int type { get; set; }
}