using System.Text;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
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
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserService(IUserRepo repository, IMapper mapper, IValidator<UserDTO> UserDtoValidator,
        IValidator<UserLoginDTO> UserLoginvalidator, IHttpContextAccessor httpContextAccessor)
    {
        _UserRepository = repository;
        _mapper = mapper;
        _UserDTOValidator = UserDtoValidator;
        _UserLoginvalidator = UserLoginvalidator;
        _httpContextAccessor = httpContextAccessor;
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
        byte[] passwordHash, passwordSalt;
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
       
        byte[] passwordHash, passwordSalt;
        User user = new User();
        user.Id = userID;
        user = _mapper.Map<User>(userDto);
        GenerateHash(userDto.password, out passwordHash, out passwordSalt);
        user.HashPassword = passwordHash;
        user.SaltPassword = passwordSalt;

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

    public void CompareHashValueHash(string Password, out byte[] PasswordHash, byte[] PasswordSalt)
    {
        using (var hmac = new System.Security.Cryptography.HMACSHA512())
        {
            PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Password));
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