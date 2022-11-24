using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Factory.Application.Interfaces;
using Factory.Application.PostProdDTO;
using Factory.Application.Validators;
using Factory.Domain;
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
        
        [HttpGet("{ManFacId}")]
        
        public ActionResult<Product> BoxOfIDFinder(int ManFacId)
        {
            try
            {
                return Ok(_shopService.GetProductByID(ManFacId));
            }
            catch (KeyNotFoundException e)
            {
                return NotFound("No product found at ID " + ManFacId);
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
                return Created("box/" + result.ID, result);
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
        [Route("{ManFacId}")]

        public ActionResult<Product> UpdateProduct([FromRoute] int ManFacId, [FromBody] Product box)
        {
            try
            {
                return Ok(_shopService.UpdateProduct(ManFacId, box));
            }
            catch (KeyNotFoundException e)
            {
                return NotFound("No box found at ID " + ManFacId);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.ToString());
            }
        }

        [HttpDelete("{ManFacId}")]

        public ActionResult<Product> DeleteProductByID(int ManFacId)
        {
            try
            {
                return Ok(_shopService.DeleteProduct(ManFacId));
            }
            catch (KeyNotFoundException e)
            {
                return NotFound("No box found at ID " + ManFacId);
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