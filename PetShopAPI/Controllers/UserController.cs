using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PetShop.Application;
using PetShop.Application.Interfaces;
using PetShop.Application.PostProdDTO;
using PetShop.Domain;

namespace PetShopApi.Controllers;
[ApiController]
[Route("[Controller]")]
public class UserController : ControllerBase
{

    private IUserService _userService;
    private readonly Authentication _authentication;
    private readonly Logic _logic;
    
    public UserController(IUserService service, Logic logic, Authentication authentication)
    {  
        _userService = service;
        _logic = logic;
        _authentication = authentication;
    }
    
    [HttpGet]
    public ActionResult<List<User>> GetAllUsers()
    {
        try
        {
            return Ok(_userService.GetAllUsers());
        }
        catch (KeyNotFoundException e)
        {
            return NotFound("No User found");
        }
        catch (Exception e)
        {
            return StatusCode(500, e.ToString());
        }
    }
    
     [HttpGet("{userID}")]
            
            public ActionResult<User> GetUserById(Guid userID)
            {
                try
                {
                    return Ok(_userService.GetUserByID(userID));
                }
                catch (KeyNotFoundException e)
                {
                    return NotFound("No User found at ID " + userID);
                }
                catch (Exception e)
                {
                    return StatusCode(500, e.ToString());
                }
            }
            
            
            [HttpPost("login")]
            public async Task<ActionResult<string>> Login(UserLoginDTO userLogin)
            {   
                
                var currentUser = _userService.GetUserByName(userLogin.UserName);
                if (currentUser.Name != userLogin.UserName)
                {
                    return BadRequest("Wrong password or username.");
                }

                if (_logic.ValidateHash(userLogin.Password, currentUser.HashPassword, currentUser.SaltPassword))
                {
                    return BadRequest("Wrong password or username.");
                }
               
                string token = _authentication.CreateToken(currentUser);
                
                return Ok(token);
            }
            
            
            [HttpPost]
            [Route("register")]
            public ActionResult<User> CreateUser(UserDTO userDto)
            {
                try
                {
                    var result = _userService.CreateUsers(userDto);
                    return Created("User/" + result.Id , result);
                }
                catch (ValidationException e)
                {
                    return BadRequest(e.Message);
                }
                catch (Exception e)
                {
                    return StatusCode(500, e.Message);
                }
            }
    
            [HttpPut]
            [Route("{userID}")]
            public ActionResult<User> UpdateUser([FromRoute] Guid userID, [FromBody] UserDTO userDto, string currentPassword)
            {
               
                var actualUser = _userService.GetUserByID(userID);
                try
                {
                    if(_logic.ValidateHash(currentPassword, actualUser.HashPassword, actualUser.SaltPassword))
                    {
                        return BadRequest("Wrong password.");
                    }
                    else
                    {
                        return Ok(_userService.UpdateUser(actualUser, userDto));
                    }
                    
                    
                }
                catch (KeyNotFoundException e)
                {
                    return NotFound("No User found at ID " + userID);
                }
                catch (Exception e)
                {
                    return StatusCode(500, e.Message);
                }
            }

            [HttpDelete("{userID}")]
            public ActionResult<User> DeleteUserById(Guid userID)
            {
                try
                {
                    return Ok(_userService.DeleteUserById(userID));
                }
                catch (KeyNotFoundException e)
                {
                    return NotFound("No specification found at ID " + userID);
                }
                catch (Exception e)
                {
                    return StatusCode(500, e.ToString());
                }
            }
            
            
}