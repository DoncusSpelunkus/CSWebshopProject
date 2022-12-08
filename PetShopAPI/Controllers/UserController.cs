using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PetShop.Application.Interfaces;
using PetShop.Application.PostProdDTO;
using PetShop.Domain;

namespace PetShopApi.Controllers;
[ApiController]
[Route("[Controller]")]
public class UserController : ControllerBase
{

    private IUserService _userService;
    private readonly IConfiguration _configuration;

    public UserController(IUserService service, IConfiguration configuration)
    {   _configuration = configuration;
        _userService = service;
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
            
            
            [HttpPost]
            [Route("login")]
            public async Task<ActionResult<string>> Login(UserLoginDTO request)
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
            [Route("{userId}")]
    
            public ActionResult<User> UpdateUser([FromRoute] Guid userID, [FromBody] UserDTO userDto)
            {
                try
                {
                    return Ok(_userService.UpdateUser(userID, userDto));
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