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
    public class SubCatController : ControllerBase
    {
        
        private ICatService _catService;

        public SubCatController(ICatService service)
        {
            _catService = service;
        }
        [HttpGet]
        public ActionResult<List<SubCategory>> GetAllSubCategories()
        {
            try
            {
                return Ok(_catService.GetAllSubCategories());
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

        [HttpGet("{subCatID}")]

        public ActionResult<SubCategory> GetSubCategoryById(int subCatID)
        {
            try
            {
                return Ok(_catService.GetSubCategoryById(subCatID));
            }
            catch (KeyNotFoundException e)
            {
                return NotFound("No category found at ID " + subCatID);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.ToString());
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult<SubCategory> CreateSubCategory(SubCatDTO dto)
        { 
            try
            {
                // Ensure the user is authenticated
                if (!User.Identity.IsAuthenticated == User.HasClaim("type", "Admin"))
                    return Unauthorized();
                else
                {
                    var result = _catService.CreateSubCategory(dto);
                    return Created("shop/" + result.SubCategoryID, result);
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
        [Route("{subCatID}")]
        [Authorize]
        public ActionResult<SubCategory> UpdateSubCategory([FromRoute] int subCatID, [FromBody] SubCatDTO subCategory)
        {
            try
            {
                // Ensure the user is authenticated
                if (!User.Identity.IsAuthenticated == User.HasClaim("type", "Admin"))
                    return Unauthorized();
                else
                {
                    return Ok(_catService.UpdateSubCategory(subCatID, subCategory));
                }
            }
            catch (KeyNotFoundException e)
            {
                return NotFound("No category found at ID " + subCatID);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.ToString());
            }
        }

        [HttpDelete("{subCatID}")]
        [Authorize]

        public ActionResult<SubCategory> DeleteSubCategoryByID(int subCatID)
        {
            try
            {
                // Ensure the user is authenticated
                if (!User.Identity.IsAuthenticated == User.HasClaim("type", "Admin"))
                    return Unauthorized();
                else
                {
                    return Ok(_catService.DeleteSubCategoryById(subCatID));
                }
            }
            catch (KeyNotFoundException e)
            {
                return NotFound("No category found at ID " + subCatID);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.ToString());
            }
        }
    }
}