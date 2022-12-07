using PetShop.Domain;
using PetShop.Application.PostProdDTO;

namespace PetShop.Application.Interfaces;

public interface IUserService
{
    public List<User> GetAllUsers();
        
    public User CreateUsers(UserDTO userDto);

    public User UpdateUser(Guid userID, User user);
        
    public User DeleteUserById(Guid userID);

    public User GetUserByID(Guid userId);
}