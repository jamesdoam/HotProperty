using System.ComponentModel.DataAnnotations;

namespace HotProperty_Web.Models.Dto
{
    public class PropertyDTO
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        public int AskingPrice { get; set; }
        public int NoBedroom { get; set; }
        public string State { get; set; }
        public string ImageUrl { get; set; }
    }
}


