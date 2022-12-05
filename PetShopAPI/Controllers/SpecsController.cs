﻿using System.ComponentModel.DataAnnotations;
using AutoMapper;
using PetShop.Application.Interfaces;
using PetShop.Application.PostProdDTO;
using PetShop.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetShop.Application.Validators;

namespace PetShopApi.Controllers
{
    [ApiController]
    [Route("specs/[Controller]")]
    public class SpecsController : ControllerBase
    {
        private SpecValidator _specsValidator;
        private IMapper _mapper;
        private ISpecService _specService;
        public SpecsController(ISpecService service, IMapper mapper)
        {
            _specService = service;
            _specsValidator = new SpecValidator();
            _mapper = mapper;
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
                return Ok(_specService.GetSpecByID(specID));
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
        public ActionResult<Specs> CreateSpecs(SpecDTO dto)
        {
            try
            {
                var result = _specService.CreateSpecs(dto);
                return Created("Spec/" + result.ID, result);
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

        public ActionResult<Specs> UpdateSpecs([FromRoute] int specID, [FromBody] Specs specs)
        {
            try
            {
                return Ok(_specService.UpdateSpecs(specID, specs));
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

        public ActionResult<Specs> DeleteSpecsById(int specID)
        {
            try
            {
                return Ok(_specService.DeleteSpecsById(specID));
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