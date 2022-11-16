using HotProperty_PropertyAPI.Data;
using HotProperty_PropertyAPI.Logging;
using HotProperty_PropertyAPI.Models;
using HotProperty_PropertyAPI.Models.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace HotProperty_PropertyAPI.Controllers
{
    [ApiController]
    [Route("api/PropertyAPI")]
    //[Route("api/[controller]") this will use the controller name in the route
    public class PropertyAPIController : ControllerBase
    {
        private readonly ILogging _logger;

        public PropertyAPIController(ILogging logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<PropertyDTO>> GetProperties()
        {
            _logger.Log("LogInfo: Getting all properties",""); //2 arguments, first is message, second is type = "" blank
            return Ok(PropertyStore.propertyList);

        }

        [HttpGet("{id:int}", Name = "GetProperty")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<PropertyDTO> GetProperty(int id)
        {
            if (id == 0)
            {
                _logger.Log("LogError: Get Property with Id = " + id, "error");
                return BadRequest();
            }

            var property = PropertyStore.propertyList.FirstOrDefault(u => u.Id == id);
            if (property == null)
            {
                _logger.Log("LogError: Property not found", "error");
                return NotFound();
            }

            return Ok(property);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<PropertyDTO> CreateProperty([FromBody] PropertyDTO propertyDTO)
        {   //check if the property name is already exist in the propertyList
            if (PropertyStore.propertyList.FirstOrDefault(u => u.Name.ToLower() == propertyDTO.Name.ToLower()) != null)
            {
                _logger.Log("LogError: Create Property with duplicated name", "error");
                ModelState.AddModelError("CustomError", "Property name already exists!");
                return BadRequest(ModelState);
            }
            if (propertyDTO == null)
            {
                return BadRequest(propertyDTO);
            }
            if (propertyDTO.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            //find the obj with largest Id and plus one for the new obj
            propertyDTO.Id = PropertyStore.propertyList.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;
            PropertyStore.propertyList.Add(propertyDTO);

            return CreatedAtRoute("GetProperty", new { id = propertyDTO.Id }, propertyDTO);
        }

        [HttpDelete("{id:int}", Name = "DeleteProperty")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult DeleteProperty(int id)
        {
            if (id == 0) //return bad request if id == 0
            {
                return BadRequest();
            }
            // if id is not 0, get the property from data store
            var property = PropertyStore.propertyList.FirstOrDefault(u => u.Id == id);

            if (property == null) //if property is not (not in the list), return not found
            {
                return NotFound();
            }
            // if property is in the list, remove it

            PropertyStore.propertyList.Remove(property);
            return NoContent(); //delete request return no content

        }

        [HttpPut("{id:int}", Name = "UpdateProperty")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult UpdateProperty(int id, [FromBody]PropertyDTO propertyDTO)
        {
            //if not valid, return bad request
            if (id != propertyDTO.Id || propertyDTO == null)
            {
                return BadRequest();
            }
            //find the property in the list and change its properties according to the input DTO
            var property = PropertyStore.propertyList.FirstOrDefault(u => u.Id == id);

            property.Name = propertyDTO.Name;
            property.NoBedroom = propertyDTO.NoBedroom;

            return NoContent();
        }

        [HttpPatch("{id:int}", Name = "UpdatePartialProperty")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult UpdatePartialProperty(int id, JsonPatchDocument<PropertyDTO> patchDTO)
        {
            if(patchDTO == null || id == 0)
            {
                return BadRequest();
            }

            var property = PropertyStore.propertyList.FirstOrDefault(u => u.Id == id);
            if(property == null)
            {
                return BadRequest();
            }

            patchDTO.ApplyTo(property, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();
        }
    }
}
