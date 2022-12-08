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
               

                var refreshToken = GenerateRefreshToken();
                SetRefreshToken(refreshToken, currentUser);

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
            
            [HttpPost("refresh-token")]
            public async Task<ActionResult<string>> RefreshToken()
            {
                var refreshToken = Request.Cookies["refreshToken"];
                var currentUser = _userService.GetUserByToken(refreshToken);
                if (!currentUser.RefreshToken.Equals(refreshToken))
                {
                    return Unauthorized("Invalid Refresh Token.");
                }
                else if(currentUser.TokenExpires < DateTime.Now)
                {
                    return Unauthorized("Token expired.");
                }

                string token = CreateToken(currentUser);
                var newRefreshToken = GenerateRefreshToken();
                SetRefreshToken(newRefreshToken, currentUser);

                return Ok(token);
            }
            
            private void SetRefreshToken(RefreshToken newRefreshToken, User currentUser)
            {
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Expires = newRefreshToken.Expires
                };
                Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);
                
                currentUser.RefreshToken = newRefreshToken.Token;
                currentUser.TokenCreated = newRefreshToken.Created;
                currentUser.TokenExpires = newRefreshToken.Expires;
            }
            private string CreateToken(User user)
            {
                List<Claim> claims = new List<Claim>
                {   // not allowed to use the api stuff for creating product if its just a basic user.
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Role, "User")
                };
                
                if (String.IsNullOrEmpty(user.Name))
                    throw new ArgumentNullException(nameof(user.Name));
                
                
                var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                    _configuration.GetSection("AppSettings:Token").Value));

                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

                var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: creds);

                var jwt = new JwtSecurityTokenHandler().WriteToken(token);

                return jwt;
            }
    
            private RefreshToken GenerateRefreshToken()
            {
                var refreshToken = new RefreshToken
                {
                    Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                    Expires = DateTime.Now.AddDays(7),
                    Created = DateTime.Now
                };

                return refreshToken;
            }   


}