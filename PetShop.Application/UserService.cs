using System.Text;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using PetShop.Application.Interfaces;
using PetShop.Application.PostProdDTO;
using PetShop.Domain;

public class UserService : IUserService
{
    private readonly IUserRepo _UserRepository;
    private readonly IMapper _mapper;
    private IValidator<UserDTO> _UserDTOValidator;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserService(IUserRepo repository, IMapper mapper, IValidator<UserDTO> UserDtoValidator, IHttpContextAccessor httpContextAccessor)
    {
        _UserRepository = repository;
        _mapper = mapper;
        _UserDTOValidator = UserDtoValidator;
       
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

    public User UpdateUser(User user, UserDTO userDto)
    {   
        // Validate the UserDTO object
        var validation = _UserDTOValidator.Validate(userDto);
        if (!validation.IsValid)
        {
            throw new ValidationException(validation.ToString());
        }

        // Update the existing User object with the data from the UserDTO object
        _mapper.Map(userDto, user);

        // Generate a new password hash and salt, if the password was changed
        if (userDto.password != null)
        {
            byte[] passwordHash, passwordSalt;
            GenerateHash(userDto.password, out passwordHash, out passwordSalt);
            user.HashPassword = passwordHash;
            user.SaltPassword = passwordSalt;
        }

        // Save the updated User object to the database
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