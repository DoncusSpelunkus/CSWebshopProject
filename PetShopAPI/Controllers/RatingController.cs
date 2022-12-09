using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetShop.Application.Interfaces;
using PetShop.Application.PostProdDTO;
using PetShop.Domain;

namespace PetShopApi.Controllers;
[ApiController]
[Route("[controller]")]
[Authorize]
public class RatingController : ControllerBase
{
   
    private IProductService _productService;
    private IMapper _mapper;
    public RatingController(IProductService service)
    {
        _productService = service;
    }
    
    
    [HttpPost("{userID} {productID}")]
    [Route("postReview")]
    [Authorize]
    public async Task<ActionResult<Rating>> CreateRating(ratingDTO ratingDto, [FromRoute] int productid, [FromRoute] string userId)
    {   
        // checking if the token holds a user
        bool hasClaim = User.HasClaim(ClaimTypes.Role, "User");
        
        // Ensure the user is authenticated
        if (!User.Identity.IsAuthenticated)
        {
            return Unauthorized();
        }

        int productidToadd = productid;
        string userIdToadd = userId;
        var rating = _mapper.Map<Rating>(ratingDto);
        if (hasClaim.Equals(true))
        {
            // Add the rating to the database
            var result =  _productService.AddRating(rating, productidToadd, userIdToadd);
            return Ok(result);
        }
        else
        {
            return Unauthorized("You need to log into to post a review");
        }
    }
    
}