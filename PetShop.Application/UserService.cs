using System.Text;
using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using PetShop.Application.Interfaces;
using PetShop.Application.PostProdDTO;
using PetShop.Domain;

namespace PetShop.Application;

public class UserService : IUserService
{
    private IUserRepo _UserRepository;
    private IMapper _mapper;
    private IValidator<UserLoginDTO> _UserLoginvalidator;
    private IValidator<UserDTO> _UserDTOValidator;
    

    public UserService(IUserRepo repository, IMapper mapper, IValidator<UserDTO> UserDtoValidator, IValidator<UserLoginDTO> UserLoginvalidator)
    {
        _UserRepository = repository;
        _mapper = mapper;
        _UserDTOValidator = UserDtoValidator;
        _UserLoginvalidator = UserLoginvalidator;
       
    }

    public List<User> GetAllUsers()
    {
        return _UserRepository.GetAllUser();
    }

    public User CreateUsers(UserDTO userDto)
    {
        var validation = _UserDTOValidator.Validate(userDto);
        if (!validation.IsValid)
        {
            throw new ValidationException(validation.ToString());
        }
        var currentUser = _mapper.Map<User>(userDto);
        byte [] passwordHash, passwordSalt;
        GenerateHash(userDto.password, out passwordHash, out passwordSalt);
        currentUser.HashPassword = passwordHash;
        currentUser.SaltPassword = passwordSalt;
        
        return _UserRepository.CreateUser(currentUser);

    }

    public User UpdateUser(Guid userID, UserDTO userDto)
    {
        var validation = _UserDTOValidator.Validate(userDto);
        if (!validation.IsValid)
        {
            throw new ValidationException(validation.ToString());
        }

        User user = new User();
        user = _mapper.Map<User>(userDto);
        user.Id = userID;
        return _UserRepository.UpdateUser(user);
    }



    public User DeleteUserById(Guid userID)
    {

        if (userID == null) throw new ValidationException("Id is invalid (Delete)");
        return _UserRepository.DeleteUser(userID);
    }

    public User GetUserByID(Guid userId)
    {
        if (userId == null) throw new ValidationException("Id is invalid (Get)");
        return _UserRepository.GetUserByID(userId);
    }

    public User UserLogin(UserLoginDTO userLoginDto)
    {
        var validation = _UserLoginvalidator.Validate(userLoginDto);
        if (!validation.IsValid)
        {
            throw new ValidationException(validation.ToString());
        }
        //var currentUser = _mapper.Map<User>(userLoginDto);
        var user = _UserRepository.GetUserByName(userLoginDto.UserName);
        if (user == null) throw new ValidationException("User not found");
        if (!ValidateHash(userLoginDto.Password, user.HashPassword, user.SaltPassword))
        {
            throw new ValidationException("Password is incorrect");
        }
        return user;
    }
    
    public void GenerateHash(string Password, out byte[] PasswordHash, out byte[] PasswordSalt)
    {
        using (var hmac = new System.Security.Cryptography.HMACSHA512())
        {
            PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Password));
            PasswordSalt = hmac.Key;

        }
    }
    
    public Boolean ValidateHash(string password, byte[] passwordhash, byte[] passwordsalt)
    {
        using (var hash = new System.Security.Cryptography.HMACSHA512(passwordsalt))
        {
            var newPassHash = hash.ComputeHash(Encoding.UTF8.GetBytes(password));
            for (int i = 0; i < newPassHash.Length; i++)
                if (newPassHash[i] != passwordhash[i])
                    return false;
        }

        return true;
    }
    
    public User GetUserByName(string userName)
    {
        return _UserRepository.GetUserByName(userName);
    }
   
    
   
    
    
    
    
}