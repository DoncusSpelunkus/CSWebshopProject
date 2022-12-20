
using PetShop.Application.Interfaces;
using PetShop.Domain;


namespace PetShop.Infastructure;

public class UserRepo : IUserRepo
{
    private DBContext _dbcontext;

    public UserRepo(DBContext dbcontext)
    {
       
        _dbcontext = dbcontext;
    }


    public List<User> GetAllUser()
    {
        return _dbcontext.UserTable.ToList();
    }

    public User GetUserById(Guid id)
    {
        return _dbcontext.UserTable.FirstOrDefault(u => u.Id == id);
    }

    public User CreateUser(User user)
    {   
        if (_dbcontext.UserTable.Any(u => u.Email.Equals(user.Email)))
        {
            throw new Exception("User with this email already exists");
        }
        _dbcontext.UserTable.Add(user);
        _dbcontext.SaveChanges();
        return user;
    }
   
    public User UpdateUser(User user)
    {
        
        _dbcontext.UserTable.Update(user);
        _dbcontext.SaveChanges();
        return user;
    }

    public User DeleteUser(Guid id)
    {
        var user = _dbcontext.UserTable.FirstOrDefault(u => u.Id == id);
        _dbcontext.UserTable.Remove(user);
        _dbcontext.SaveChanges();
        return user;
    }

    public User GetUserByEmail(string currentEmail)
    {   
      
        return _dbcontext.UserTable.FirstOrDefault(u => u.Email == currentEmail);
    }

    public string GetUserId(string userId)
    {   
        User? id = _dbcontext.UserTable.FirstOrDefault(u => u.Id.ToString().Equals(userId));
        return id.ToString();
    }
}