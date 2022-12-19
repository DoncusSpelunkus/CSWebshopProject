using AutoMapper;
using PetShop.Domain;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PetShop.Application.Interfaces;
using PetShop.Application.PostProdDTO;
using PetShop.Application.Validators;

namespace PetShopApi.Controllers{
    [ApiController]
    [Route("[Controller]")]
    public class BrandController : ControllerBase
    {
        private IBrandService _brandService;

        public BrandController(IBrandService service)
        {
            _brandService = service;
        }

        [HttpGet]
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

        public ActionResult<MainCategory> UpdateBrand([FromRoute] int brandID, [FromBody] BrandDto brandDto)
        {
            try
            {
                return Ok(_brandService.UpdateBrand(brandID , brandDto));
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