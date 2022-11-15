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
        public IEnumerable<PropertyDTO> GetProperties()
        {
            return PropertyStore.propertyList;

        }
        [HttpGet("{id:int}")]
        public PropertyDTO GetProperty(int id)
        {
            return PropertyStore.propertyList.FirstOrDefault(u => u.Id == id);
        }
    }
}
