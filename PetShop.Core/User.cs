namespace PetShop.Domain;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string passwordSalt { get; set; }
    public int type { get; set; }
}