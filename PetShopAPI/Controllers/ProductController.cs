using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using AutoMapper;
using PetShop.Application.Interfaces;
using PetShop.Application.PostProdDTO;
using PetShop.Application.Validators;
using PetShop.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PetShopApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class ProductController : ControllerBase
    {
        private ProdValidator _productValidator;
        private IMapper _mapper;
        private IProductService _productService;
        public ProductController(IProductService service, IMapper mapper)
        
        {
            _productService = service;
           
        }

        [HttpGet]
        public ActionResult<List<Product>> GetProduct()
        {
            try
            {
                return Ok(_productService.GetAllProducts());
            }
            catch (KeyNotFoundException e)
            {
                return NotFound("No product found");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.ToString());
            }
        }
        
        [HttpGet("{productID}")]
        
        public ActionResult<Product> GetProductById(int productID)
        {
            try
            {
                return Ok(_productService.GetProductById(productID));
            }
            catch (KeyNotFoundException e)
            {
                return NotFound("No product found at ID " + productID);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.ToString());
            }
        }
        
        [HttpPost]
        [Authorize]
        public ActionResult<Product> CreateProduct(ProdDTO dto)
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
                    var result = _productService.CreateProduct(dto);
                    return Created("shop/" + result.ID, result);
                }
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
        [Route("{productID}")]
        [Authorize]
        public ActionResult<Product> UpdateProduct([FromRoute] int productID, [FromBody] ProdDTO dto)
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
                    return Ok(_productService.UpdateProduct(productID, dto));
                }
            }
            catch (KeyNotFoundException e)
            {
                return NotFound("No product found at ID " + productID);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.ToString());
            }
        }

        [HttpDelete("{productID}")]
        [Authorize]
        public ActionResult<Product> DeleteProductByID(int productID)
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
                    return Ok(_productService.DeleteProduct(productID));
                }
            }
            catch (KeyNotFoundException e)
            {
                return NotFound("No product found at ID " + productID);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.ToString());
            }
        }
        
        [HttpGet]
        [Route("RebuildDB")]
      
        public void RebuildDB()

        {
            // checking if the token holds an admin
           
           
            
                // Ensure the user is authenticated
                
                    _productService.RebuildDB();
               
            
        }
    }
}