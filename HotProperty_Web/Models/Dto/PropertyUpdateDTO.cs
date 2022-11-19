using System.ComponentModel.DataAnnotations;

namespace HotProperty_Web.Models.Dto
{
    public class PropertyUpdateDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        [Required]
        public int AskingPrice { get; set; }
        [Required] 
        public int NoBedroom { get; set; }
        [Required]
        public string State { get; set; }
        public string ImageUrl { get; set; }
    }
}


