using Factory.Domain;
using PetShop.Application.PostProdDTO;

namespace PetShop.Application.Interfaces;

public class IUserService
{
    public List<User> GetAllSpecs();
        
    public User CreateSpecs(UserDTO userDto);

    public User UpdateSpecs(int userID, User user);
        
    public User DeleteSpecsById(int userID);

    public User GetSpecByID(int userId);
}