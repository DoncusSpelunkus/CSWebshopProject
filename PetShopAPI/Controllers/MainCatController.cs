using System.Security.Claims;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
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
    public class MainCatController : ControllerBase
    {
        private ActualMainCatValidator.MainCatValidator _mainCatValidator;
        private IMapper _mapper;
        private ICatService _catService;

        public MainCatController(ICatService service, IMapper mapper)
        {
            _catService = service;
            _mainCatValidator = new ActualMainCatValidator.MainCatValidator();
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<List<MainCategory>> GetAllMainCategories()
        {
            try
            {
                return Ok(_catService.GetAllMainCategories());
            }
            catch (KeyNotFoundException e)
            {
                return NotFound("No category found");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.ToString());
            }
        }

        [HttpGet("{mainCatID}")]
        
        public ActionResult<MainCategory> GetMainCategoryById(int mainCatID)
        {
            try
            {
                return Ok(_catService.GetMainCategoryById(mainCatID));
            }
            catch (KeyNotFoundException e)
            {
                return NotFound("No category found at ID " + mainCatID);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.ToString());
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult<MainCategory> CreateMainCategory(MainCatDTO dto)
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
                    var result = _catService.CreateMainCategory(dto);
                    return Created("shop/" + result.MainCategoryID, result);
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
        [Route("{mainCatID}")]
        [Authorize]

        public ActionResult<MainCategory> UpdateMainCategory([FromRoute] int mainCatID, [FromBody] MainCatDTO mainCategory)
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
                    return Ok(_catService.UpdateMainCategory(mainCatID, mainCategory));
                }
            }
            catch (KeyNotFoundException e)
            {
                return NotFound("No category found at ID " + mainCatID);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.ToString());
            }
        }

        [HttpDelete("{mainCatID}")]
        [Authorize]

        public ActionResult<MainCategory> DeleteMainCategoryByID(int mainCatID)
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
                    return Ok(_catService.DeleteMainCategoryById(mainCatID));
                }
            }
            catch (KeyNotFoundException e)
            {
                return NotFound("No category found at ID " + mainCatID);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.ToString());
            }
        }
        
    }
}
