using PetShop.Domain;

namespace PetShop.Application.Interfaces;

public interface IUserRepo
{
    
    public List<User> GetAllUser();
    public User GetUserById(Guid id);
    public User CreateUser(User user);
    public User UpdateUser(User user);
    public User DeleteUser(Guid id);
    public User GetUserByEmail(string currentUserEmail);
    public string GetUserId(string userId);
}