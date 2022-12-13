using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PetShop.Domain;

namespace PetShop.Application;

public class Authentication
{   
    private readonly IConfiguration _configuration;

    public Authentication(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public string CreateToken(User user)
    {
        List<Claim> claims = new List<Claim>();

        var userRole = "";

        if (user.type == 1)
        {
            userRole = "User";
        }

        if (user.type == 2)
        {
            userRole = "Admin";
        }
        
        claims.Add(new Claim("id", user.Id.ToString()));
        claims.Add(new Claim("name", user.Name)); ;
        claims.Add(new Claim("email",user.Email));
        claims.Add(new Claim("type", userRole));
        claims.Add(new Claim("city", user.City));
        claims.Add(new Claim("address", user.Address));
        claims.Add(new Claim("zip", user.Zip.ToString()));
        claims.Add(new Claim("phone", user.Phone.ToString()));
            
            // creating token for admin, 2 = admin
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