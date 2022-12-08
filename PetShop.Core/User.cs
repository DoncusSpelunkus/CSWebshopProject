namespace PetShop.Domain;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public byte[] HashPassword { get; set; }
    public byte[] SaltPassword { get; set; }
    public int type { get; set; }
    
    public DateTime TokenCreated { get; set; }
    public DateTime TokenExpires { get; set; }

}