using System.Security.Claims;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public ActionResult<List<User>> GetAllUsers()
    {
        // checking if the token holds an admin
        bool hasClaim = User.HasClaim(ClaimTypes.Role, "Admin");
        try
        {
            // Ensure the user is authenticated
            if (!User.Identity.IsAuthenticated)
                return Unauthorized();
            else
            {
                return Ok(_userService.GetAllUsers());
            }
                
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
                    return Ok(_userService.GetUserById(userID));
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
                
                var currentUser = _userService.GetUserByEmail(userLogin.Email);
                if (currentUser.Email != userLogin.Email)
                {
                    return BadRequest("Wrong password or Email.");
                }

                if (!_logic.ValidateHash(userLogin.Password, currentUser.HashPassword, currentUser.SaltPassword))
                {
                    return BadRequest("Wrong password or Email.");
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
            [Route("update")]
            public ActionResult<User> UpdateUser(Guid userID, [FromBody] UpdateUserDTO userDto, string currentPassword)
            {
               
                var actualUser = _userService.GetUserById(userID);
                try
                {
                    if(!_logic.ValidateHash(currentPassword, actualUser.HashPassword, actualUser.SaltPassword))
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