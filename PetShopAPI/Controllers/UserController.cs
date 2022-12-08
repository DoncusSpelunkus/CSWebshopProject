using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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
            
            
            [HttpPost("login")]
            public async Task<ActionResult<string>> Login(UserLoginDTO userLogin)
            {   
                
                var currentUser = _userService.GetUserByName(userLogin.UserName);
                if (currentUser.Name != userLogin.UserName)
                {
                    return BadRequest("User not found.");
                }

                if (!_userService.ValidateHash(userLogin.Password, currentUser.HashPassword, currentUser.SaltPassword))
                {
                    return BadRequest("Wrong password.");
                }
               
                string token = CreateToken(currentUser);
                
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
    
            private string CreateToken(User user)
            {
                List<Claim> claims = new List<Claim>();

                if (user.type == 2){// not allowed to use the api stuff for creating product if its just a basic user.
                    claims.Add(new Claim(ClaimTypes.Name, user.Name));
                   claims.Add(new Claim(ClaimTypes.Role, "Admin"));
                    
                }
                else if (user.type== 1)
                {
                    claims.Add(new Claim(ClaimTypes.Name, user.Name));
                    claims.Add(new Claim(ClaimTypes.Role, "User"));
                }
                // creting token for admin, 2 = admin
                if (String.IsNullOrEmpty(user.Name))
                    throw new ArgumentNullException(nameof(user.Name));
                
                
                var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                    _configuration.GetSection("AppSettings:Token").Value));

                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

                var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(7),
                    signingCredentials: creds);

                var jwt = new JwtSecurityTokenHandler().WriteToken(token);

                return jwt;
            }
}