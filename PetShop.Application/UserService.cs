using System.Text;
using AutoMapper;
using FluentValidation;
using PetShop.Application.Interfaces;
using PetShop.Application.PostProdDTO;
using PetShop.Domain;

namespace PetShop.Application;

public class UserService : IUserService
{ 
    private IUserRepo _UserRepository;
    private IMapper _mapper;
    private IValidator<User> _Uservalidator;
    private IValidator<UserDTO> _UserDTOValidator;
    
    public UserService(IUserRepo repository, IMapper mapper, IValidator<User> uservalidator,IValidator<UserDTO> UserDtoValidator) 
    {
        _UserRepository = repository;
        _mapper = mapper;
        _Uservalidator = uservalidator;
        _UserDTOValidator = UserDtoValidator;
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

        return _UserRepository.CreateUser(_mapper.Map<User>(userDto));

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
    
    public void GenerateHash(string Password, out byte[] PasswordHash, out byte[] PasswordSalt)
    {
        using (var hmac = new System.Security.Cryptography.HMACSHA512())
        {
            PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Password));
            PasswordSalt = hmac.Key;
            
        }
    }
    private Boolean ValidateHash(string password, byte[] passwordhash, byte[] passwordsalt)
    {
        using(var hash=new System.Security.Cryptography.HMACSHA512(passwordsalt))
        {
            var newPassHash = hash.ComputeHash(Encoding.UTF8.GetBytes( password));
            for (int i = 0; i < newPassHash.Length; i++)
                if (newPassHash[i] != passwordhash[i])
                    return false;
        }
        return true;

    
}