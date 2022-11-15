using HotProperty_PropertyAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotProperty_PropertyAPI.Controllers
{
    [ApiController]
    [Route("api/PropertyAPI")]
    public class PropertyAPIController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Property> GetProperties()
        {
            return new List<Property>()
            {
                new Property{Id=1, Name = "34 Arnold St"},
                new Property{Id=2, Name = "14 Davey St"}
            };

        }
    }
}
