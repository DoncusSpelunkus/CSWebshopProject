﻿
using System.Text;
using PetShop.Application.Interfaces;
using PetShop.Application.PostProdDTO;
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

    public User GetUserByID(Guid id)
    {
        return _dbcontext.UserTable.FirstOrDefault(u => u.Id == id);
    }

    public User CreateUser(User user)
    {   if(_dbcontext.UserTable.Any(u => u.Name.Equals(user.Name)))
        {
            throw new Exception("User already exists");
        }
        if (_dbcontext.UserTable.Any(u => u.Email.Equals(user.Email)))
        {
            throw new Exception("Email already exists");
        }
        _dbcontext.UserTable.Add(user);
        _dbcontext.SaveChanges();
        return user;
    }
   // method used for creating user in the database
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

    public User GetUserByName(string currentUserName)
    {   
      
        return _dbcontext.UserTable.FirstOrDefault(u => u.Name == currentUserName);
    }
    
}