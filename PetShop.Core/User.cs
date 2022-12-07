namespace Factory.Domain;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public Byte[] PasswordHash { get; set; }
    public Byte[] passwordSalt { get; set; }
    public int type { get; set; }
}