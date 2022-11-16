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

        public PropertyAPIController(ILogging logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PropertyDTO>>> GetProperties()
        {
            _logger.Log("LogInfo: Getting all properties",""); //2 arguments, first is message, second is type = "" blank
            return Ok(await _db.Properties.ToListAsync());

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

            return Ok(property);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PropertyDTO>> CreateProperty([FromBody] PropertyCreateDTO propertyDTO)
        {   //check if the property name is already exist in the propertyList
            if (await _db.Properties.FirstOrDefaultAsync(u => u.Name.ToLower() == propertyDTO.Name.ToLower()) != null)
            {
                _logger.Log("LogError: Create Property with duplicated name", "error");
                ModelState.AddModelError("CustomError", "Property name already exists!");
                return BadRequest(ModelState);
            }
            if (propertyDTO == null)
            {
                return BadRequest(propertyDTO);
            }
            //No need to check Id because CreateDTO doesn't have Id field
            //if (propertyDTO.Id > 0)
            //{
            //    return StatusCode(StatusCodes.Status500InternalServerError);
            //}
            //create a new Property obj from PropertyDTO and save it to localdb
            Property propertyObj = new()
            {
                Name = propertyDTO.Name,
                State = propertyDTO.State,
                AskingPrice = propertyDTO.AskingPrice,
                NoBedroom = propertyDTO.NoBedroom,
                ImageUrl = propertyDTO.ImageUrl
            };
            await _db.Properties.AddAsync(propertyObj);
            await _db.SaveChangesAsync();

            return CreatedAtRoute("GetProperty", new { id = propertyObj.Id }, propertyObj);
        }

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

        [HttpPut("{id:int}", Name = "UpdateProperty")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateProperty(int id, [FromBody]PropertyUpdateDTO propertyDTO)
        {
            //if not valid, return bad request
            if (id != propertyDTO.Id || propertyDTO == null)
            {
                return BadRequest();
            }

            //create a new property object from the propertyDto and update it to the database
            Property propertyObj = new()
            {
                Id = propertyDTO.Id,
                Name = propertyDTO.Name,
                State = propertyDTO.State,
                AskingPrice = propertyDTO.AskingPrice,
                NoBedroom = propertyDTO.NoBedroom,
                ImageUrl = propertyDTO.ImageUrl
            };

            _db.Properties.Update(propertyObj);
            await _db.SaveChangesAsync();
            return NoContent();
        }

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

            PropertyUpdateDTO propertyDTO = new()
            {
                Id = propertyObj.Id,
                Name = propertyObj.Name,
                State = propertyObj.State,
                AskingPrice = propertyObj.AskingPrice,
                NoBedroom = propertyObj.NoBedroom,
                ImageUrl = propertyObj.ImageUrl
            };

            if (propertyObj == null)
            {
                return BadRequest();
            }

            //then apply patchDTO to the new DTO object
            patchDTO.ApplyTo(propertyDTO, ModelState);

            //if valid, create a new property obj from the patched DTO
            //then update the database and finally save!
            Property newPropertyObj = new Property()
            {
                Id = propertyDTO.Id,
                Name = propertyDTO.Name,
                State = propertyDTO.State,
                AskingPrice = propertyDTO.AskingPrice,
                NoBedroom = propertyDTO.NoBedroom,
                ImageUrl = propertyDTO.ImageUrl
            };

            //propertyObj.Id = propertyDTO.Id;
            //propertyObj.Name = propertyDTO.Name;
            //propertyObj.State = propertyDTO.State;
            //propertyObj.AskingPrice = propertyDTO.AskingPrice;
            //propertyObj.NoBedroom = propertyDTO.NoBedroom;
            //propertyObj.ImageUrl= propertyDTO.ImageUrl;


            _db.Properties.Update(newPropertyObj);
            await _db.SaveChangesAsync();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return NoContent();
        }
    }
}
