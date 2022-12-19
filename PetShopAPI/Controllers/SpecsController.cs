using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using PetShop.Application.Interfaces;
using PetShop.Application.PostProdDTO;
using PetShop.Domain;
using Microsoft.AspNetCore.Mvc;


namespace PetShopApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class SpecsController : ControllerBase
    {
        
        private ISpecService _specService;
        public SpecsController(ISpecService service)
        {
            _specService = service;
        }

        [HttpGet]
        public ActionResult<List<Specs>> GetAllSpecs()
        {
            try
            {
                return Ok(_specService.GetAllSpecs());
            }
            catch (KeyNotFoundException e)
            {
                return NotFound("No specification found");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.ToString());
            }
        }
        
        [HttpGet("{specID}")]
        public ActionResult<Specs> GetSpecById(int specID)
        {
            try
            {
                return Ok(_specService.GetSpecById(specID));
            }
            catch (KeyNotFoundException e)
            {
                return NotFound("No specification found at ID " + specID);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.ToString());
            }
        }
        
        [HttpPost]
        [Authorize]
        public ActionResult<Specs> CreateSpecs(SpecDTO dto)
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
                    var result = _specService.CreateSpecs(dto);
                    return Created("Spec/" + result.ID, result);
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
        [Route("{specID}")]
        [Authorize]
        public ActionResult<Specs> UpdateSpecs([FromRoute] int specID, [FromBody] SpecDTO dto)
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
                    return Ok(_specService.UpdateSpecs(specID, dto));
                }
            }
            catch (KeyNotFoundException e)
            {
                return NotFound("No Specification found at ID " + specID);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.ToString());
            }
        }

        [HttpDelete("{specID}")]
        [Authorize]
        public ActionResult<Specs> DeleteSpecsById(int specID)
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
                    return Ok(_specService.DeleteSpecsById(specID));
                }
            }
            catch (KeyNotFoundException e)
            {
                return NotFound("No specification found at ID " + specID);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.ToString());
            }
        }
        
    }
    
}