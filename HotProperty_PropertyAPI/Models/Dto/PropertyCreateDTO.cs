using System.ComponentModel.DataAnnotations;

namespace HotProperty_PropertyAPI.Models.Dto
{
    public class PropertyCreateDTO
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        [Required]
        public int AskingPrice { get; set; }
        public int NoBedroom { get; set; }
        public string State { get; set; }
        public string ImageUrl { get; set; }
    }
}


