using HotProperty_PropertyAPI.Data;
using HotProperty_PropertyAPI.Models;
using HotProperty_PropertyAPI.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace HotProperty_PropertyAPI.Controllers
{
    [ApiController]
    [Route("api/PropertyAPI")]
    //[Route("api/[controller]") this will use the controller name in the route
    public class PropertyAPIController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<PropertyDTO>> GetProperties()
        {
            return Ok(PropertyStore.propertyList);

        }
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<PropertyDTO> GetProperty(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var property = PropertyStore.propertyList.FirstOrDefault(u => u.Id == id); 
            if (property==null)
            {
                return NotFound();
            }

            return Ok(property);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<PropertyDTO> CreateProperty([FromBody]PropertyDTO propertyDTO)
        {
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

            return Ok(propertyDTO);
        }
    }
}
