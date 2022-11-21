using AutoMapper;
using HotProperty_Web.Models;
using HotProperty_Web.Models.Dto;
using HotProperty_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HotProperty_Web.Controllers
{
    public class PropertyNumberController : Controller
    {
        private readonly IPropertyNumberService _propertyNumberService;
        private readonly IMapper _mapper;

        public PropertyNumberController(IPropertyNumberService propertyNumberService, IMapper mapper)
        {
            _propertyNumberService = propertyNumberService;
            _mapper = mapper; 
        }

        // ~~~~****** PROPERTY INDEX ACTION - GET ******~~~~ //
        public async Task<IActionResult> PropertyNumberIndex()
        {
            List<PropertyNumberDTO> list = new();
            //use the GetAllAsync method from PropertyService and
            //assign the response to the reponse variable, data type APIResponse. 
            var response = await _propertyNumberService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess) //refer to BaseService.cs
            {
                //convert the response's result to a list of PropertyDTO and return to the View. 
                list = JsonConvert.DeserializeObject<List<PropertyNumberDTO>>(Convert.ToString(response.Result));
            }

            return View(list);
        }

        //// ~~~~****** PROPERTY CREATE ACTION - GET ******~~~~ //
        //public async Task<IActionResult> PropertyCreate()
        //{
        //    return View();
        //}
        //// ~~~~****** PROPERTY CREATE ACTION - POST ******~~~~ //
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> PropertyCreate(PropertyCreateDTO createDTO)
        //{
        //    //check if the createDTO is valid, 
        //    if (ModelState.IsValid)
        //    {
        //        //if so, send a request for Creating a new property, this method takes the createDTO as the input argument as per PropertyService
        //        var response = await _propertyService.CreateAsync<APIResponse>(createDTO);
        //        if (response != null && response.IsSuccess)
        //        {
        //            //if the response is success, i.e. a property has been created, return to Property Index
        //            return RedirectToAction(nameof(PropertyIndex));
        //        }
        //    }
        //    return View(createDTO); //if not valid, stay at PropertyCreate view with all parameters the same!
        //}

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
