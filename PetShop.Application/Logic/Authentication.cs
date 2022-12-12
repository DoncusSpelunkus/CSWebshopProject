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

        if (user.type == 2){
            // not allowed to use the api stuff for creating product if its just a basic user.
            claims.Add(new Claim(ClaimTypes.Name, user.Name));
            claims.Add(new Claim(ClaimTypes.Role, "Admin"));
            claims.Add(new Claim(ClaimTypes.Email,user.Email));
            claims.Add(new Claim(ClaimTypes.MobilePhone, user.PhoneNumber.ToString()));
            claims.Add(new Claim(ClaimTypes.StreetAddress, user.Address));
            claims.Add(new Claim(ClaimTypes.PostalCode, user.Zipcode.ToString()));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.StateOrProvince, user.City));
            
                    
        }
        else if (user.type== 1)
        {
            claims.Add(new Claim(ClaimTypes.Name, user.Name));
            claims.Add(new Claim(ClaimTypes.Role, "User"));
            claims.Add(new Claim(ClaimTypes.Email,user.Email));
            claims.Add(new Claim(ClaimTypes.MobilePhone, user.PhoneNumber.ToString()));
            claims.Add(new Claim(ClaimTypes.StreetAddress, user.Address));
            claims.Add(new Claim(ClaimTypes.PostalCode, user.Zipcode.ToString()));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.StateOrProvince, user.City));
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