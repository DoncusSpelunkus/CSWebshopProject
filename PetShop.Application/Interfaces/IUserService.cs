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
    public User UserLogin(UserLoginDTO userLoginDto);
    public User GetUserByName(string userName);
}