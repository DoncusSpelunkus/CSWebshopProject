namespace PetShop.Application.PostProdDTO;

public class UserDTO
{
    public string Name { get; set; }
    public string Email { get; set; }
    public Byte[] PasswordHash { get; set; }
    public Byte[] passwordSalt { get; set; }
    public int type { get; set; }
}