﻿using AutoMapper;
using HotProperty_PropertyAPI.Data;
using HotProperty_PropertyAPI.Logging;
using HotProperty_PropertyAPI.Models;
using HotProperty_PropertyAPI.Models.Dto;
using HotProperty_PropertyAPI.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Runtime.CompilerServices;

namespace HotProperty_PropertyAPI.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/PropertyAPI")]
    [ApiVersion("1.0")]
    //[Route("api/[controller]") this will use the controller name in the route
    public class PropertyAPIController : ControllerBase
    {
        private readonly ILogging _logger;
        private readonly IPropertyRepository _dbProperty;
        private readonly IMapper _mapper;
        protected APIResponse _response;

        public PropertyAPIController(ILogging logger, IPropertyRepository dbProperty, IMapper mapper)
        {
            _logger = logger;
            _dbProperty = dbProperty;
            _mapper = mapper;
            _response = new();
        }

        // ********************** GET ALL PROPERTIES *************************//
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //instead of returning IEnumerable of Property, this time return APIResponse
        public async Task<ActionResult<APIResponse>> GetProperties()
        {
            try
            {
                //get a list of all properties in the DB and then map them to DTOs and add to response object.
                IEnumerable<Property> propertyList = await _dbProperty.GetAllAsync();
                _response.Result = _mapper.Map<List<PropertyDTO>>(propertyList);
                _response.StatusCode = HttpStatusCode.OK;
                _logger.Log("LogInfo: Getting all properties succesfully", "success"); //2 arguments, first is message, second is type = "" blank
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        // ********************** GET 1 PROPERTY *************************//
        [HttpGet("{id:int}", Name = "GetProperty")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<APIResponse>> GetProperty(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _logger.Log("LogError: Get Property with Id = " + id, "error");
                    return BadRequest(_response);
                }

                var property = await _dbProperty.GetAsync(u => u.Id == id);

                if (property == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _logger.Log("LogError: Property not found", "error");
                    return NotFound(_response);
                }
                //map property object to propertyDTO object and add to response obj
                _response.Result = _mapper.Map<PropertyDTO>(property);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { ex.ToString() };
            }

            return _response;
        }

        // ********************** CREATE PROPERTY *************************//
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateProperty([FromBody] PropertyCreateDTO createDTO)
        {
            try
            {
                //check if the property name is already exist in the propertyList
                if (await _dbProperty.GetAsync(u => u.Name.ToLower() == createDTO.Name.ToLower()) != null)
                {
                    _logger.Log("LogError: Create Property with duplicated name", "error");
                    ModelState.AddModelError("ErrorMessages", "Property name already exists!");
                    return BadRequest(ModelState);
                }
                if (createDTO == null)
                {
                    _logger.Log("LogError: I'm not sure what's going on here", "error");
                    return BadRequest(createDTO);
                }

                //map the createDTO to property object and save it to the db.
                Property propertyObj = _mapper.Map<Property>(createDTO);
                await _dbProperty.CreateAsync(propertyObj);
                _response.Result = _mapper.Map<PropertyDTO>(propertyObj);
                _response.StatusCode = HttpStatusCode.Created;

                _logger.Log("LogInfo: A New Property has been succesfully created", "success");
                return CreatedAtRoute("GetProperty", new { id = propertyObj.Id }, _response);
            }

            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { ex.ToString() };
            }

            return _response;
        }

        // ********************** DELETE PROPERTY *************************//
        [Authorize(Roles = "admin")] //only role = CUSTOM is authorized
        [HttpDelete("{id:int}", Name = "DeleteProperty")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<APIResponse>> DeleteProperty(int id)
        {
            try
            {
                if (id == 0) //return bad request if id == 0
                {
                    return BadRequest();
                }
                // if id is not 0, get the property from data store
                var property = await _dbProperty.GetAsync(u => u.Id == id);

                if (property == null) //if property is not (not in the list), return not found
                {
                    return NotFound();
                }
                // if property is in the list, remove it

                await _dbProperty.RemoveAsync(property);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response); //delete request return no content
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string> { ex.ToString() };
            }

            return _response;
        }

        // ********************** UPDATE PROPERTY *************************//
        [Authorize(Roles = "admin")]
        [HttpPut("{id:int}", Name = "UpdateProperty")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<APIResponse>> UpdateProperty(int id, [FromBody] PropertyUpdateDTO updateDTO)
        {
            try
            {
                if (id != updateDTO.Id || updateDTO == null)
                {
                    //if not valid, return bad request
                    return BadRequest();
                }
                // map the DTO to propertyObj and update the DB
                Property propertyObj = _mapper.Map<Property>(updateDTO);

                await _dbProperty.UpdateAsync(propertyObj);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string> { ex.ToString() };
            }

            return _response;
        }

        // ********************** UPDATE PARTIAL PROPERTY *************************//
        [HttpPatch("{id:int}", Name = "UpdatePartialProperty")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdatePartialProperty(int id, JsonPatchDocument<PropertyUpdateDTO> patchDTO)
        {
            if (patchDTO == null || id == 0)
            {
                return BadRequest();
            }
            //retrive the property from database from the id provided
            //if not null, create a new propertyDTO from the real object
            var propertyObj = await _dbProperty.GetAsync(u => u.Id == id, tracked: false);

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
            await _dbProperty.UpdateAsync(newPropertyObj);
            return NoContent();
        }
    }
}
