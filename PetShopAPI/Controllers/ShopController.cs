using System.ComponentModel.DataAnnotations;
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
    public class ShopController : ControllerBase
    {
        private ProdValidator _productValidator;
        private IMapper _mapper;
        private IShopService _shopService;
        public ShopController(IShopService service, IMapper mapper)
        {
            _shopService = service;
            _productValidator = new ProdValidator();
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<List<Product>> GetProduct()
        {
            try
            {
                return Ok(_shopService.GetAllProducts());
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
                return Ok(_shopService.GetProductByID(productID));
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

        public ActionResult<Product> CreateProduct(ProdDTO dto)
        {
            try
            {
                var result = _shopService.CreateProduct(dto);
                return Created("shop/" + result.ID, result);
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

        public ActionResult<Product> UpdateProduct([FromRoute] int productID, [FromBody] Product product)
        {
            try
            {
                return Ok(_shopService.UpdateProduct(productID, product));
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

        [HttpDelete("{ManFacId}")]

        public ActionResult<Product> DeleteProductByID(int productID)
        {
            try
            {
                return Ok(_shopService.DeleteProduct(productID));
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
        
        
        [AllowAnonymous]
        [HttpGet]
        [Route("RebuildDB")]
        public void RebuildDB()
        {
            _shopService.RebuildDB();
        }
        
        
    }
    
    
    
}