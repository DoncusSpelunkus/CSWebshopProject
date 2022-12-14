using PetShop.Domain;
using PetShop.Application.PostProdDTO;

namespace PetShop.Application.Interfaces;

public interface IUserService
{
    public List<User> GetAllUsers();
        
    public User CreateUsers(UserDTO userDto);

    public User UpdateUser(User user, UpdateUserDTO userDto);
        
    public User DeleteUserById(Guid userId);

    public User GetUserById(Guid userId);
 
    public User GetUserByEmail(string userEmail);
    
    
}