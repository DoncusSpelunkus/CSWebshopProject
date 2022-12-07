using Factory.Domain;

namespace PetShop.Application.Interfaces;

public class IUserRepo
{
    public User GetUser(string username);
    public User GetUser(string username, string password);
    public void InsertUser(User user);
    public void UpdateUser(User user);
    public void DeleteUser(User user);
}