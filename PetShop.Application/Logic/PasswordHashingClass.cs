using System.Text;

namespace PetShop.Application;

public class Logic
{   
    
    // need to be virtual for mocking and testing
    public virtual void GenerateHash(string Password, out byte[] PasswordHash, out byte[] PasswordSalt)
    {
        using (var hmac = new System.Security.Cryptography.HMACSHA512())
        {
            PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Password));
            PasswordSalt = hmac.Key;

        }
    }
    public Boolean ValidateHash(string password, byte[] passwordhash, byte[] passwordsalt)
    {
        using (var hash = new System.Security.Cryptography.HMACSHA512(passwordsalt))
        {
            var newPassHash = hash.ComputeHash(Encoding.UTF8.GetBytes(password));
            for (int i = 0; i < newPassHash.Length; i++)
                if (newPassHash[i] != passwordhash[i])
                    return false;
        }

        
        return true;
    }
}