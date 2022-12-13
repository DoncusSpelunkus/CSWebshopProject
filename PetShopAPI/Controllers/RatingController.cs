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
    
    
    [HttpPost]
    [Route("postRewiev, {userId}, {productId}")]
    [Authorize]
    public async Task<ActionResult<Rating>> CreateRating(ratingDTO ratingDto, [FromRoute] int productId, [FromRoute] Guid userId)
    {   
        // checking if the token holds a user
        bool hasClaim = User.HasClaim(ClaimTypes.Role, "User");
        
        // Ensure the user is authenticated
        if (!User.Identity.IsAuthenticated)
        {
            return Unauthorized();
        }
        if (hasClaim.Equals(true))
        {
            // Add the rating to the database
            return Ok(_productService.AddRating(ratingDto, productId, userId ));
        }
        else
        {
            return Unauthorized("You need to login to post a review");
        }
    }

    [HttpPut]
    [Route("updateRating")]
    [Authorize]
    public async Task<ActionResult<Rating>> UpdateRating(ratingDTO ratingDto)
    {
        if (ratingDto.RatingValue < 0 || ratingDto.RatingValue > 5)
        {
            return BadRequest("Rating value must be between 0 and 5");
        }
        
        var RatingToUpdate = _mapper.Map<Rating>(ratingDto);
        return Ok(_productService.UpdateRating(RatingToUpdate));
    }
    
    [HttpGet]
    [Route("getRating")]
    public async Task<ActionResult<Rating>> GetRating([FromQuery] int ratingId)
    {
        return Ok(_productService.GetTheAverageRatingForProduct(ratingId));
    }
    
}