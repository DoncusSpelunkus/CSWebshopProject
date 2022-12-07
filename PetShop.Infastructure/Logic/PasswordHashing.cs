using System.Security.Cryptography;
using System.Text;
using PetShop.Domain;

namespace PetShop.Infastructure.Logic;

public class PasswordHashing
{
    public string HashPassword(string password)
    {
        // SHA = secure hash algorithm 
        SHA256 hash = SHA256.Create();
        // converts the userpassword to an aray of bytes
        var passwordbBytes = Encoding.Default.GetBytes(password);
        var hashedPassword = hash.ComputeHash(passwordbBytes);

       return Convert.ToHexString(hashedPassword);
       
    }
}