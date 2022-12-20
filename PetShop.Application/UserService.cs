using System.Text;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using PetShop.Application;
using PetShop.Application.Interfaces;
using PetShop.Application.PostProdDTO;
using PetShop.Domain;

public class UserService : IUserService
{
    private readonly IUserRepo _UserRepository;
    private readonly IMapper _mapper;
    private IValidator<UserDTO> _UserDTOValidator;
    private IValidator<UpdateUserDTO> _UpdateUserDTOValidator;
    private readonly Logic _logic;
   

    public UserService(IUserRepo repository, IMapper mapper, IValidator<UserDTO> UserDtoValidator, Logic logic, IValidator<UpdateUserDTO> updateUserDtoValidator)
    {
        _UserRepository = repository;
        _mapper = mapper;
        _UserDTOValidator = UserDtoValidator;
        _logic = logic;
        _UpdateUserDTOValidator = updateUserDtoValidator;
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
        _logic.GenerateHash(userDto.password, out passwordHash, out passwordSalt);
        currentUser.HashPassword = passwordHash;
        currentUser.SaltPassword = passwordSalt;
        return _UserRepository.CreateUser(currentUser);

    }

    public User UpdateUser(User user, UpdateUserDTO userDto)
    {   
        // Validate the UserDTO object
        var validation = _UpdateUserDTOValidator.Validate(userDto);
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
            _logic.GenerateHash(userDto.password, out passwordHash, out passwordSalt);
            user.HashPassword = passwordHash;
            user.SaltPassword = passwordSalt;
        }

        // Save the updated User object to the database
        return _UserRepository.UpdateUser(user);
    }
    


    public User DeleteUserById(Guid userId)
    {

        if (userId == null) throw new ValidationException("Id is invalid (Delete)");
        return _UserRepository.DeleteUser(userId);
    }

    public User GetUserById(Guid userId)
    {
        if (userId == null) throw new ValidationException("Id is invalid (Get)");
        return _UserRepository.GetUserById(userId);
    }
    

    public User GetUserByEmail(string userEmail)
    {
        
        return _UserRepository.GetUserByEmail(userEmail);
    }







}