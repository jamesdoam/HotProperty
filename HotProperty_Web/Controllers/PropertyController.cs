using AutoMapper;
using HotProperty_Web.Models;
using HotProperty_Web.Models.Dto;
using HotProperty_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HotProperty_Web.Controllers
{
    public class PropertyController : Controller
    {
        private readonly IPropertyService _propertyService;
        private readonly IMapper _mapper;

        public PropertyController(IPropertyService propertyService, IMapper mapper)
        {
            _propertyService = propertyService;
            _mapper = mapper; 
        }

        public async Task<IActionResult> PropertyIndex()
        {
            List<PropertyDTO> list = new();
            //use the GetAllAsync method from PropertyService and
            //assign the response to the reponse variable, data type APIResponse. 
            var response = await _propertyService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess) //refer to BaseService.cs
            {
                //convert the response's result to a list of PropertyDTO and return to the View. 
                list = JsonConvert.DeserializeObject<List<PropertyDTO>>(Convert.ToString(response.Result));
            }

            return View(list);
        }
    }
}
