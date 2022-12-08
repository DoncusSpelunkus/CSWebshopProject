using PetShop.Domain;
using PetShop.Application.PostProdDTO;

namespace PetShop.Application.Interfaces;

public interface IUserService
{
    public List<User> GetAllUsers();
        
    public User CreateUsers(UserDTO userDto);

    public User UpdateUser(Guid userID, UserDTO userDto);
        
    public User DeleteUserById(Guid userID);

    public User GetUserByID(Guid userId);
 
    public User GetUserByName(string userName);
    public Boolean ValidateHash(string password, byte[] passwordhash, byte[] passwordsalt);
    public void GenerateHash(string Password, out byte[] PasswordHash, out byte[] PasswordSalt);

    public void CompareHashValueHash(string Password, out byte[] PasswordHash, byte[] PasswordSalt);


}