using AutoMapper;
using HotProperty_Web.Models;
using HotProperty_Web.Models.Dto;
using HotProperty_Web.Models.VM;
using HotProperty_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HotProperty_Web.Controllers
{
    public class PropertyNumberController : Controller
    {
        private readonly IPropertyNumberService _propertyNumberService;
        private readonly IPropertyService _propertyService;
        private readonly IMapper _mapper;

        public PropertyNumberController(IPropertyNumberService propertyNumberService, IPropertyService propertyService, IMapper mapper)
        {
            _propertyNumberService = propertyNumberService;
            _propertyService = propertyService;
            _mapper = mapper; 
        }

        // ~~~~****** PROPERTY NUMBER INDEX ACTION - GET ******~~~~ //
        public async Task<IActionResult> PropertyNumberIndex()
        {
            List<PropertyNumberDTO> list = new();            
            var response = await _propertyNumberService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess) //refer to BaseService.cs
            {                
                list = JsonConvert.DeserializeObject<List<PropertyNumberDTO>>(Convert.ToString(response.Result));
            }

            return View(list);
        }

        // ~~~~****** PROPERTY CREATE ACTION - GET ******~~~~ //
        public async Task<IActionResult> PropertyNumberCreate()
        {
            PropertyNumberCreateVM propertyNumberVM = new();
            var response = await _propertyService.GetAllAsync<APIResponse>();
            if (response !=null && response.IsSuccess)
            {
                propertyNumberVM.PropertyList = JsonConvert.DeserializeObject<List<PropertyDTO>>(Convert.ToString(response.Result)).Select(i => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                });
                ;
            }
            return View(propertyNumberVM);
        }
        // ~~~~****** PROPERTY NUMBER CREATE ACTION - POST ******~~~~ //
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PropertyNumberCreate(PropertyNumberCreateVM createDTO)
        {
            //check if the createDTO is valid, 
            if (ModelState.IsValid)
            {
                var response = await _propertyNumberService.CreateAsync<APIResponse>(createDTO.PropertyNumber);
                if (response != null && response.IsSuccess)
                {
                    //if the response is success, i.e. a property has been created, return to Property Number Index
                    return RedirectToAction(nameof(PropertyNumberIndex));
                }
            }
            return View(createDTO); //if not valid, stay at PropertyCreate view with all parameters the same!
        }

        //// ~~~~****** PROPERTY UPDATE ACTION - GET******~~~~ //
        //public async Task<IActionResult> PropertyUpdate(int propertyId)
        //{
        //    //get the info of the Property using the Id and display in the form, get ready for edit
        //    var response = await _propertyService.GetAsync<APIResponse>(propertyId);
        //    //remember from the Property Service, the GetAsync method take an ID as input argument
        //    if (response != null&& response.IsSuccess)
        //    {
        //        PropertyDTO propertyDTO = JsonConvert.DeserializeObject<PropertyDTO>(Convert.ToString(response.Result));
        //        //convert DTO to Update DTO and return to the view, the model for update DTO and standard DTO maybe different!
        //        return View(_mapper.Map<PropertyUpdateDTO>(propertyDTO));
        //    }

        //    return NotFound();
        //}

        //// ~~~~****** PROPERTY UPDATE ACTION - POST ******~~~~ //
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> PropertyUpdate(PropertyUpdateDTO updateDTO)
        //{
        //    if(ModelState.IsValid)
        //    {
        //        var response = await _propertyService.UpdateAsync<APIResponse>(updateDTO);
        //        if (response !=null && response.IsSuccess)
        //        {
        //            return RedirectToAction(nameof(PropertyIndex));
        //        }
        //    }
        //    //if not valid, stay at PropertyUpdate view with all parameters the same!
        //    return View(updateDTO);
        //}

        //// ~~~~****** PROPERTY DELETE ACTION - GET******~~~~ //
        //public async Task<IActionResult> PropertyDelete(int propertyId)
        //{
        //    //get the info of the Property using the Id and display in the form, get ready for edit
        //    var response = await _propertyService.GetAsync<APIResponse>(propertyId);
        //    //remember from the Property Service, the GetAsync method take an ID as input argument
        //    if (response != null && response.IsSuccess)
        //    {
        //        PropertyDTO propertyDTO = JsonConvert.DeserializeObject<PropertyDTO>(Convert.ToString(response.Result));
        //        //return to the view with the DTO details!
        //        return View(propertyDTO);
        //    }

        //    return NotFound();
        //}

        //// ~~~~****** PROPERTY DELETE ACTION - POST ******~~~~ //
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> PropertyDelete(PropertyDTO propertyDTO)
        //{ 
        //    var response = await _propertyService.DeleteAsync<APIResponse>(propertyDTO.Id);
        //    if (response != null && response.IsSuccess)
        //    {
        //        //if deletion is success, return to the list of properties
        //        return RedirectToAction(nameof(PropertyIndex));
        //    }
        //    //if not valid, stay at Property Delete view with all parameters the same!
        //    return View(propertyDTO);
        //}
    }
}
