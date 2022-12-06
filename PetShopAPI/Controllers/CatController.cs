using AutoMapper;
using Factory.Domain;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PetShop.Application;
using PetShop.Application.Interfaces;
using PetShop.Application.PostProdDTO;
using PetShop.Application.Validators;
using PetShop.Domain;

namespace PetShopApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class CatController : ControllerBase
    {
        private ActualMainCatValidator.MainCatValidator _mainCatValidator;
        private ActualSubCatValidator.SubCatValidator _subCatValidator;
        private IMapper _mapper;
        private ICatService _catService;

        public CatController(ICatService service, IMapper mapper)
        {
            _catService = service;
            _mainCatValidator = new ActualMainCatValidator.MainCatValidator();
            _subCatValidator = new ActualSubCatValidator.SubCatValidator();
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetAllMainCategories")]
        public ActionResult<List<MainCategory>> GetAllMainCategories()
        {
            try
            {
                return Ok(_catService.GetAllMainCategories());
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
        
        

        [HttpGet("{mainCategoryID}")]
        
        public ActionResult<MainCategory> GetMainCategoryById(int mainCategoryID)
        {
            try
            {
                return Ok(_catService.GetMainCategoryById(mainCategoryID));
            }
            catch (KeyNotFoundException e)
            {
                return NotFound("No product found at ID " + mainCategoryID);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.ToString());
            }
        }

        [HttpPost]
        public ActionResult<MainCategory> CreateMainCategory(MainCatDTO dto)
        {
            try
            {
                var result = _catService.CreateMainCategory(dto);
                return Created("shop/" + result.RefID, result);
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
        [Route("{mainCatID}")]

        public ActionResult<MainCategory> UpdateMainCategory([FromRoute] int mainCatID, [FromBody] MainCategory mainCategory)
        {
            try
            {
                return Ok(_catService.UpdateMainCategory(mainCatID,mainCategory));
            }
            catch (KeyNotFoundException e)
            {
                return NotFound("No product found at ID " + mainCatID);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.ToString());
            }
        }

        [HttpDelete("{mainCatID}")]

        public ActionResult<MainCategory> DeleteMainCategoryByID(int mainCatID)
        {
            try
            {
                return Ok(_catService.DeleteMainCategoryById(mainCatID));
            }
            catch (KeyNotFoundException e)
            {
                return NotFound("No product found at ID " + mainCatID);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.ToString());
            }
        }
        
        [HttpGet]
        [Route("GetAllSubCategories")]
        public ActionResult<List<SubCategory>> GetAllSubCategories()
        {
            try
            {
                return Ok(_catService.GetAllSubCategories());
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
        
        [HttpGet("{subCategoryID}")]
        
        public ActionResult<SubCategory> GetSubCategoryById(int subCategoryID)
        {
            try
            {
                return Ok(_catService.GetSubCategoryById(subCategoryID));
            }
            catch (KeyNotFoundException e)
            {
                return NotFound("No product found at ID " + subCategoryID);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.ToString());
            }
        }

        [HttpPost]
        public ActionResult<SubCategory> CreateSubCategory(SubCatDTO dto)
        {
            try
            {
                var result = _catService.CreateSubCategory(dto);
                return Created("shop/" + result.RefID, result);
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
        [Route("{subCatID}")]

        public ActionResult<SubCategory> UpdateSubCategory([FromRoute] int subCatID, [FromBody] SubCategory subCategory)
        {
            try
            {
                return Ok(_catService.UpdateSubCategory(subCatID,subCategory));
            }
            catch (KeyNotFoundException e)
            {
                return NotFound("No product found at ID " + subCatID);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.ToString());
            }
        }

        [HttpDelete("{subCatID}")]

        public ActionResult<SubCategory> DeleteSubCategoryByID(int subCatID)
        {
            try
            {
                return Ok(_catService.DeleteSubCategoryById(subCatID));
            }
            catch (KeyNotFoundException e)
            {
                return NotFound("No product found at ID " + subCatID);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.ToString());
            }
        }
    }
}
