using AutoMapper;
using HotProperty_Web.Models;
using HotProperty_Web.Models.Dto;
using HotProperty_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace HotProperty_Web.Controllers
{
    public class HomeController : Controller
    {        
        private readonly IPropertyService _propertyService;
        private readonly IMapper _mapper;

        public HomeController(IPropertyService propertyService, IMapper mapper)
        {
            _propertyService = propertyService;
            _mapper = mapper;  
        }

        public async Task<IActionResult> Index()
        {
            List<PropertyDTO> propertyList = new();
            var response = await _propertyService.GetAllAsync<APIResponse>();
            if (response!=null&&response.IsSuccess)
            {
                propertyList = JsonConvert.DeserializeObject<List<PropertyDTO>>(Convert.ToString(response.Result));
            }

            return View(propertyList);
        }
    }
}