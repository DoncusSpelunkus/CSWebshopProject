using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PetShop.Application.Interfaces;
using PetShop.Application.PostProdDTO;
using PetShop.Domain;

namespace PetShopApi.Controllers;
[ApiController]
[Route("[controller]")]
public class RatingController : ControllerBase
{
   
    private IProductService _productService;
    private IMapper _mapper;
    public RatingController(IProductService service)
    {
        _productService = service;
    }
    
    
    [HttpPost]
    [Route("postReview")]
    public async Task<ActionResult<Rating>> CreateRating(ratingDTO ratingDto, int productId, Guid userId)
    {
        if (productId == 0 || userId == null)
        {
            return BadRequest();
        }
        {
            return Ok(_productService.AddRating(ratingDto, productId, userId));
        }
            
        
       
    }

    [HttpPut]
    [Route("updateRating")]
    public async Task<ActionResult<Rating>> UpdateRating(ratingDTO ratingDto, int productId, Guid userId)
    {
        if (ratingDto.RatingValue < 0 || ratingDto.RatingValue > 5)
        {
            return BadRequest("Rating value must be between 0 and 5");
        }

        // Check that the productId and userId parameters are valid.
        if (productId <= 0 || userId == null)
        {
            return BadRequest("Invalid product or user ID");
        }
        
        // Set the ProductId and UserId properties of the ratingToUpdate object.
        return Ok(_productService.UpdateRating(ratingDto, productId, userId));
    }
    
    [HttpGet]
    [Route("getRating")]
    public async Task<ActionResult<Rating>> GetRating(int ProductId)
    {
        return Ok(_productService.GetTheAverageRatingForProduct(ProductId));
    }
    
}