using System.ComponentModel.DataAnnotations;

namespace HotProperty_Web.Models.Dto
{
    public class PropertyNumberCreateDTO
    {
        [Required]
        public int PropertyNo { get; set; }

        [Required]
        public int PropertyID { get; set; }
        public string SpecialDetails { get; set; }
    }
}
