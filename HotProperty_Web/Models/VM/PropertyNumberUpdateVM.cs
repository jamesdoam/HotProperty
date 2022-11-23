using HotProperty_Web.Models.Dto;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HotProperty_Web.Models.VM
{
    public class PropertyNumberUpdateVM
    {
        public PropertyNumberUpdateVM()
        {
            PropertyNumber = new PropertyNumberUpdateDTO();
        }
        public PropertyNumberUpdateDTO PropertyNumber { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> PropertyList { get; set; }
    }
}