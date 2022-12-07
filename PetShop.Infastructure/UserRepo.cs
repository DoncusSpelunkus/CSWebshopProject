using System.Security.Cryptography;
using System.Text;
using PetShop.Application.Interfaces;
using PetShop.Domain;
using PetShop.Infastructure.Logic;

namespace PetShop.Infastructure;

public class UserRepo : IUserRepo
{
    private DBContext _dbcontext;
    private PasswordHashing _passwordHashing;
    public UserRepo(DBContext dbcontext, PasswordHashing passwordHashing)
    {
        _passwordHashing = passwordHashing;
        _dbcontext = dbcontext;
    }


    public List<User> GetAllUser()
    {
        return _dbcontext.Usertable.ToList();
    }

    public User GetUserByID(Guid id)
    {
        return _dbcontext.Usertable.FirstOrDefault(u => u.Id == id);
    }

    public User CreateUser(User user)
    {   
        _passwordHashing.HashPassword(user.PasswordHash);
        _dbcontext.Usertable.Add(user);
        _dbcontext.SaveChanges();
        return user;
    }
   // method used for creating user in the database
    public User UpdateUser(User user)
    {
       
        _dbcontext.Usertable.Update(user);
        _dbcontext.SaveChanges();
        return user;
    }

    public User DeleteUser(Guid id)
    {
        var user = _dbcontext.Usertable.FirstOrDefault(u => u.Id == id);
        _dbcontext.Usertable.Remove(user);
        _dbcontext.SaveChanges();
        return user;
    }
    
    // method used for verifying user login
    public User VerifyUser(string username, string password)
    {
        var user = _dbcontext.Usertable.FirstOrDefault(u => u.Name == username);
        if (user == null)
        {
            throw new Exception("User not found");
        }
        if (!_passwordHashing.HashPassword(password).Equals(user.PasswordHash))
        {
            throw new Exception("Wrong password");
        }
        
        return user;
    }
}