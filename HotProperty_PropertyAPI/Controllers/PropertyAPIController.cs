﻿using AutoMapper;
using HotProperty_PropertyAPI.Data;
using HotProperty_PropertyAPI.Logging;
using HotProperty_PropertyAPI.Models;
using HotProperty_PropertyAPI.Models.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace HotProperty_PropertyAPI.Controllers
{
    [ApiController]
    [Route("api/PropertyAPI")]
    //[Route("api/[controller]") this will use the controller name in the route
    public class PropertyAPIController : ControllerBase
    {
        private readonly ILogging _logger;
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public PropertyAPIController(ILogging logger, ApplicationDbContext db, IMapper mapper)
        {
            _logger = logger;
            _db = db;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PropertyDTO>>> GetProperties()
        {
            _logger.Log("LogInfo: Getting all properties",""); //2 arguments, first is message, second is type = "" blank
            //get a list of all properties in the DB and then map them to DTOs and return
            IEnumerable<Property> propertyList = await _db.Properties.ToListAsync();
            return Ok(_mapper.Map<List<PropertyDTO>>(propertyList));

        }

        [HttpGet("{id:int}", Name = "GetProperty")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PropertyDTO>> GetProperty(int id)
        {
            if (id == 0)
            {
                _logger.Log("LogError: Get Property with Id = " + id, "error");
                return BadRequest();
            }

            var property = await _db.Properties.FirstOrDefaultAsync(u => u.Id == id);
            if (property == null)
            {
                _logger.Log("LogError: Property not found", "error");
                return NotFound();
            }
            //map property object to propertyDTO object and return
            return Ok(_mapper.Map<PropertyDTO>(property));
        }

// ********************** CREATE PROPERTY *************************//
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PropertyDTO>> CreateProperty([FromBody] PropertyCreateDTO createDTO)
        {   //check if the property name is already exist in the propertyList
            if (await _db.Properties.FirstOrDefaultAsync(u => u.Name.ToLower() == createDTO.Name.ToLower()) != null)
            {
                _logger.Log("LogError: Create Property with duplicated name", "error");
                ModelState.AddModelError("CustomError", "Property name already exists!");
                return BadRequest(ModelState);
            }
            if (createDTO == null)
            {
                return BadRequest(createDTO);
            }
            
            //map the createDTO to property object and save it to the db.
            Property propertyObj = _mapper.Map<Property>(createDTO);
            await _db.Properties.AddAsync(propertyObj);
            await _db.SaveChangesAsync();

            return CreatedAtRoute("GetProperty", new { id = propertyObj.Id }, propertyObj);
        }

// ********************** DELETE PROPERTY *************************//
        [HttpDelete("{id:int}", Name = "DeleteProperty")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteProperty(int id)
        {
            if (id == 0) //return bad request if id == 0
            {
                return BadRequest();
            }
            // if id is not 0, get the property from data store
            var property = await _db.Properties.FirstOrDefaultAsync(u => u.Id == id);

            if (property == null) //if property is not (not in the list), return not found
            {
                return NotFound();
            }
            // if property is in the list, remove it

            _db.Properties.Remove(property);
            await _db.SaveChangesAsync();
            return NoContent(); //delete request return no content

        }

// ********************** UPDATE PROPERTY *************************//
        [HttpPut("{id:int}", Name = "UpdateProperty")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateProperty(int id, [FromBody]PropertyUpdateDTO updateDTO)
        {
            //if not valid, return bad request
            if (id != updateDTO.Id || updateDTO == null)
            {
                return BadRequest();
            }
            // map the DTO to propertyObj and update the DB
            Property propertyObj = _mapper.Map<Property>(updateDTO);

            _db.Properties.Update(propertyObj);
            await _db.SaveChangesAsync();
            return NoContent();
        }

// ********************** UPDATE PARTIAL PROPERTY *************************//
        [HttpPatch("{id:int}", Name = "UpdatePartialProperty")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdatePartialProperty(int id, JsonPatchDocument<PropertyUpdateDTO> patchDTO)
        {
            if(patchDTO == null || id == 0)
            {
                return BadRequest();
            }
            //retrive the property from database from the id provided
            //if not null, create a new propertyDTO from the real object
            var propertyObj = await _db.Properties.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);

            if (propertyObj == null)
            {
                return BadRequest();
            }

            //map the property obj retrieved from DB to UpdateDTO
            PropertyUpdateDTO propertyDTO = _mapper.Map<PropertyUpdateDTO>(propertyObj);

            //then apply patchDTO to the new DTO object
            patchDTO.ApplyTo(propertyDTO, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //if valid, map the patched DTO back to a new propertyObject, then update the database and finally save!
            Property newPropertyObj = _mapper.Map<Property>(propertyDTO);
            _db.Properties.Update(newPropertyObj);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
