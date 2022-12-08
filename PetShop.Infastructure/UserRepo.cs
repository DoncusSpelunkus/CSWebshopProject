
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
        return _dbcontext.Usertable.ToList();
    }

    public User GetUserByID(Guid id)
    {
        return _dbcontext.Usertable.FirstOrDefault(u => u.Id == id);
    }

    public User CreateUser(User user)
    {
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
    
   
    
   
}