using AutoMapper;
using Factory.Domain;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PetShop.Application.Interfaces;
using PetShop.Application.PostProdDTO;
using PetShop.Application.Validators;

namespace PetShopApi.Controllers{
    [ApiController]
    [Route("Brand/[Controller]")]
    public class BrandController : ControllerBase
    {
        
        private BrandValidator _brandValidator;
        private IMapper _mapper;
        private IBrandService _brandService;

        public BrandController(IBrandService service, IMapper mapper)
        {
            _brandService = service;
            _brandValidator = new BrandValidator();
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetAllBrands")]
        public ActionResult<List<Brand>> GetAllBrands()
        {
            try
            {
                return Ok(_brandService.GetAllBrands());
            }
            catch (KeyNotFoundException e)
            {
                return NotFound("No brand found");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.ToString());
            }
        }

        [HttpGet("{brandID}")]
        
        public ActionResult<Brand> GetBrandById(int brandID)
        {
            try
            {
                return Ok(_brandService.GetBrandByID(brandID));
            }
            catch (KeyNotFoundException e)
            {
                return NotFound("No brand found at ID " + brandID);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.ToString());
            }
        }

        [HttpPost]
        public ActionResult<Brand> CreateBrand(BrandDto dto)
        {
            try
            {
                var result = _brandService.CreateBrand(dto);
                return Created("shop/" + result.Id, result);
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
        [Route("{brandID}")]

        public ActionResult<MainCategory> UpdateBrand([FromRoute] int brandID, [FromBody] Brand brand)
        {
            try
            {
                return Ok(_brandService.UpdateBrand(brandID , brand));
            }
            catch (KeyNotFoundException e)
            {
                return NotFound("No brand found at ID " + brandID);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.ToString());
            }
        }

        [HttpDelete("{brandID}")]

        public ActionResult<Brand> DeleteBrandByID(int brandID)
        {
            try
            {
                return Ok(_brandService.DeleteBrand(brandID));
            }
            catch (KeyNotFoundException e)
            {
                return NotFound("No brand found at ID " + brandID);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.ToString());
            }
        }
    }
}