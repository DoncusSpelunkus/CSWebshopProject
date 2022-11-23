using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Factory.Core;
using Factory.Application.Interfaces;
using Factory.Application.PostBoxDTO;
using Factory.Application.Validators;
using Microsoft.AspNetCore.Mvc;

namespace Factory_of_the_Boxes.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class BoxController : ControllerBase
    {
        private BoxValidator _boxValidator;
        private IMapper _mapper;
        private IBoxService _boxService;
        public BoxController(IBoxService service, IMapper mapper)
        {
            _boxService = service;
            _boxValidator = new BoxValidator();
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<List<Box>> GetBoxes()
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
        
        public ActionResult<Box> BoxOfIDFinder(int ManFacId)
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

        public ActionResult<Box> CreateNewBox(BoxDTO dto)
        {
            try
            {
                var result = _boxService.insertBox(dto);
                return Created("box/" + result.ManFacId, result);
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

        public ActionResult<Box> UpdateBox([FromRoute] int ManFacId, [FromBody] Box box)
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

        public ActionResult<Box> DeleteBox(int ManFacId)
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