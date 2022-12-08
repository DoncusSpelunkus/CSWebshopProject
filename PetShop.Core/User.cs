using System.ComponentModel.DataAnnotations;

namespace PetShop.Domain;

public class User
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public byte[] HashPassword { get; set; }
    public byte[] SaltPassword { get; set; }
    public int type { get; set; }
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime TokenCreated{ get; set; }
    public DateTime TokenExpires { get; set; }

}