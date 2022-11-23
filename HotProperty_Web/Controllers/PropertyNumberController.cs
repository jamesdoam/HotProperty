using AutoMapper;
using HotProperty_Web.Models;
using HotProperty_Web.Models.Dto;
using HotProperty_Web.Models.VM;
using HotProperty_Web.Services;
using HotProperty_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
                else
                {
                    if (response.ErrorMessages.Count > 0)
                    {
                        ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                    }
                }
            }

            var resp = await _propertyService.GetAllAsync<APIResponse>();
            if (resp !=null &resp.IsSuccess)
            {
                createDTO.PropertyList = JsonConvert.DeserializeObject<List<PropertyDTO>>(Convert.ToString(resp.Result)).Select(i => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }); ;
            }
            return View(createDTO); //if not valid, stay at PropertyCreate view with all parameters the same!
        }

        // ~~~~****** PROPERTY NUMBER UPDATE ACTION - GET ******~~~~ //
        public async Task<IActionResult> PropertyNumberUpdate(int propertyNo)
        {
            PropertyNumberUpdateVM propertyNumberVM = new();
            var response = await _propertyNumberService.GetAsync<APIResponse>(propertyNo);
            if (response != null && response.IsSuccess)
            {
                PropertyNumberDTO model = JsonConvert.DeserializeObject<PropertyNumberDTO>(Convert.ToString(response.Result));
                propertyNumberVM.PropertyNumber = _mapper.Map<PropertyNumberUpdateDTO>(model);
            }

            response = await _propertyService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                propertyNumberVM.PropertyList = JsonConvert.DeserializeObject<List<PropertyDTO>>
                    (Convert.ToString(response.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    });
                return View(propertyNumberVM);
            }

            return NotFound();
        }

        // ~~~~****** PROPERTY NUMBER UPDATE ACTION - POST ******~~~~ //
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PropertyNumberUpdate(PropertyNumberUpdateVM model)
        {
            if (ModelState.IsValid)
            {

                var response = await _propertyNumberService.UpdateAsync<APIResponse>(model.PropertyNumber);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(PropertyNumberIndex));
                }
                else
                {
                    if (response.ErrorMessages.Count > 0)
                    {
                        ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                    }
                }
            }

            var resp = await _propertyService.GetAllAsync<APIResponse>();
            if (resp != null && resp.IsSuccess)
            {
                model.PropertyList = JsonConvert.DeserializeObject<List<PropertyDTO>>
                    (Convert.ToString(resp.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    }); ;
            }
            return View(model);
        }

        //// ~~~~****** PROPERTY DELETE ACTION - GET******~~~~ //
        public async Task<IActionResult> PropertyNumberDelete(int propertyNo)
        {
            PropertyNumberDeleteVM propertyNumberVM = new();
            var response = await _propertyNumberService.GetAsync<APIResponse>(propertyNo);
            if (response != null && response.IsSuccess)
            {
                PropertyNumberDTO model = JsonConvert.DeserializeObject<PropertyNumberDTO>(Convert.ToString(response.Result));
                propertyNumberVM.PropertyNumber = model;
            }

            response = await _propertyService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                propertyNumberVM.PropertyList = JsonConvert.DeserializeObject<List<PropertyDTO>>
                    (Convert.ToString(response.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    });
                return View(propertyNumberVM);
            }
            return NotFound();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PropertyNumberDelete(PropertyNumberDeleteVM model)
        {

            var response = await _propertyNumberService.DeleteAsync<APIResponse>(model.PropertyNumber.PropertyNo);
            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(PropertyNumberIndex));
            }

            return View(model);
        }
    }
}
