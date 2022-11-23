using AutoMapper;
using HotProperty_PropertyAPI.Data;
using HotProperty_PropertyAPI.Logging;
using HotProperty_PropertyAPI.Models;
using HotProperty_PropertyAPI.Models.Dto;
using HotProperty_PropertyAPI.Repository.IRepository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Runtime.CompilerServices;

namespace HotProperty_PropertyAPI.Controllers
{
    [ApiController]
    [Route("api/PropertyNumberAPI")]
    //[Route("api/[controller]") this will use the controller name in the route
    public class PropertyNumberAPIController : ControllerBase
    {
        private readonly ILogging _logger;
        private readonly IPropertyNumberRepository _dbPropertyNumber;
        private readonly IPropertyRepository _dbProperty;
        private readonly IMapper _mapper;
        protected APIResponse _response;

        public PropertyNumberAPIController(ILogging logger, IMapper mapper, IPropertyNumberRepository dbPropertyNumber, IPropertyRepository dbProperty)
        {
            _logger = logger;            
            _mapper = mapper;
            _dbPropertyNumber = dbPropertyNumber;
            _dbProperty = dbProperty;
            this._response = new();
        }

// ********************** GET ALL PROPERTY NUMBERS *************************//
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //instead of returning IEnumerable of Property, this time return APIResponse
        public async Task<ActionResult<APIResponse>> GetPropertyNumbers()
        {
            try
            {
                //get a list of all properties in the DB and then map them to DTOs and add to response object.
                IEnumerable<PropertyNumber> propertyNumberList = await _dbPropertyNumber.GetAllAsync(includeProperties: "Property");
                _response.Result = _mapper.Map<List<PropertyNumberDTO>>(propertyNumberList);
                _response.StatusCode = HttpStatusCode.OK;
                _logger.Log("LogInfo: Getting all property numbers succesfully", "success"); //2 arguments, first is message, second is type = "" blank
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { ex.ToString() };
            }
            return _response;
        }

// ********************** GET 1 PROPERTY NUMBER*************************//
        [HttpGet("{id:int}", Name = "GetPropertyNumber")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetPropertyNumber(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _logger.Log("LogError: Get Property with Id = " + id, "error");
                    return BadRequest(_response);
                }

                var propertyNumber = await _dbPropertyNumber.GetAsync(u => u.PropertyNo == id);
                
                if (propertyNumber == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _logger.Log("LogError: Property Number not found", "error");
                    return NotFound(_response);
                }
                //map property object to propertyDTO object and add to response obj
                _response.Result = _mapper.Map<PropertyNumberDTO>(propertyNumber);
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

// ********************** CREATE PROPERTY NUMBER*************************//
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreatePropertyNumber([FromBody] PropertyNumberCreateDTO createDTO)
        {
            try
            {
                //check if the Property Number is already exist in the propertyList
                if (await _dbPropertyNumber.GetAsync(u => u.PropertyNo == createDTO.PropertyNo) != null)
                {
                    _logger.Log("LogError: Create Property Number with duplicated Number", "error");
                    ModelState.AddModelError("CustomError", "Property Number already exists!");
                    return BadRequest(ModelState);
                }
                //check if there is a property in the database with id = PropertyID from the CreateDTO
                if (await _dbProperty.GetAsync(u=>u.Id == createDTO.PropertyID)==null)
                {
                    ModelState.AddModelError("CustomError", "PropertyID is invalid!");
                    return BadRequest(ModelState);
                }

                if (createDTO == null)
                {
                    _logger.Log("LogError: I'm not sure what's going on here", "error");
                    return BadRequest(createDTO);
                }

                //map the createDTO to property object and save it to the db.
                PropertyNumber propertyNumberObj = _mapper.Map<PropertyNumber>(createDTO);
                await _dbPropertyNumber.CreateAsync(propertyNumberObj);
                _response.Result = _mapper.Map<PropertyNumberDTO>(propertyNumberObj);
                _response.StatusCode = HttpStatusCode.Created;

                _logger.Log("LogInfo: A New Property Number has been succesfully created", "success");
                return CreatedAtRoute("GetPropertyNumber", new { id = propertyNumberObj.PropertyNo }, _response);
            }
            
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { ex.ToString() };
            }

            return _response;   
        }

// ********************** DELETE PROPERTY *************************//
        [HttpDelete("{id:int}", Name = "DeletePropertyNumber")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<APIResponse>> DeletePropertyNumber(int id)
        {
            try
            {
                if (id == 0) //return bad request if id == 0
                {
                    return BadRequest();
                }
                // if id is not 0, get the property number from data store
                var propertyNumberObj = await _dbPropertyNumber.GetAsync(u => u.PropertyNo == id);

                if (propertyNumberObj == null) //if property is not (not in the list), return not found
                {
                    return NotFound();
                }
                // if property is in the list, remove it

                await _dbPropertyNumber.RemoveAsync(propertyNumberObj);
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

// ********************** UPDATE PROPERTY NUMBER *************************//
        [HttpPut("{id:int}", Name = "UpdatePropertyNumber")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<APIResponse>> UpdatePropertyNumber(int id, [FromBody] PropertyNumberUpdateDTO updateDTO)
        {
            try
            {
                if (id != updateDTO.PropertyNo || updateDTO == null)
                {
                    //if not valid, return bad request
                    return BadRequest();
                }

                //check if there is a property in the database with id = PropertyID from the CreateDTO
                if (await _dbProperty.GetAsync(u => u.Id == updateDTO.PropertyID) == null)
                {
                    ModelState.AddModelError("CustomError", "PropertyID is invalid!");
                    return BadRequest(ModelState);
                }

                // map the DTO to propertyObj and update the DB
                PropertyNumber propertyNumberObj = _mapper.Map<PropertyNumber>(updateDTO);

                await _dbPropertyNumber.UpdateAsync(propertyNumberObj);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);

            }
            catch(Exception ex) 
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string> { ex.ToString() };
            }            
                   
            return _response;
        }
    }
}
