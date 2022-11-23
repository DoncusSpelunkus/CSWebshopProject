using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Factory.Application.Interfaces;
using Factory.Application.PostProdDTO;
using Factory.Application.Validators;
using Factory.Domain;
using Microsoft.AspNetCore.Mvc;

namespace PetShopApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class ShopController : ControllerBase
    {
        private ProdValidator _boxValidator;
        private IMapper _mapper;
        private IShopService _boxService;
        public ShopController(IShopService service, IMapper mapper)
        {
            _boxService = service;
            _boxValidator = new ProdValidator();
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<List<Product>> GetBoxes()
        {
            try
            {
                return Ok(_boxService.GetAllBoxes());
            }
            catch (KeyNotFoundException e)
            {
                return NotFound("No boxes found");
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
                return Ok(_boxService.BoxOfIDFinder(ManFacId));
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
        
        [HttpPost]

        public ActionResult<Product> CreateNewBox(ProdDTO dto)
        {
            try
            {
                var result = _boxService.insertBox(dto);
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

        public ActionResult<Product> UpdateBox([FromRoute] int ManFacId, [FromBody] Product box)
        {
            try
            {
                return Ok(_boxService.BoxUpdate(ManFacId, box));
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

        public ActionResult<Product> DeleteBox(int ManFacId)
        {
            try
            {
                return Ok(_boxService.BoxDelete(ManFacId));
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
    }
}