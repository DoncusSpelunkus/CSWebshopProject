using PetShop.Domain;

namespace PetShop.Application.Interfaces;

public interface IUserRepo
{
    
    public List<User> GetAllUser();
    public User GetUserByID(Guid id);
    public User CreateUser(User user);
    public User UpdateUser(User user);
    public User DeleteUser(Guid id);
    public User GetUserByName(string currentUserName);
  
}