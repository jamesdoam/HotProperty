using System.ComponentModel.DataAnnotations;

namespace HotProperty_PropertyAPI.Models.Dto
{
    public class PropertyNumberUpdateDTO
    {
        [Required]
        public int PropertyNo { get; set; } 
        public string SpecialDetails { get; set; }
    }
}
