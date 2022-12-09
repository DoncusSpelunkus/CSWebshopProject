using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
    
    
    [HttpPost("{userID}")]
    [Route("postReview")]
    [Authorize]
    public async Task<ActionResult<Rating>> CreateRating(ratingDTO ratingDto)
    {   bool hasClaim = User.HasClaim(ClaimTypes.Role, "User");
        
        
        // Ensure the user is authenticated
        if (!User.Identity.IsAuthenticated)
        {
            return Unauthorized();
        }
        var userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        var rating = _mapper.Map<Rating>(ratingDto);
        if (hasClaim.Equals(true))
        {
            // Add the rating to the database
            var result =  _productService.AddRating(rating);
            return Ok(result);
        }
        else
        {
            return Unauthorized("You need to log into to post a review");
        }
    }
    
}