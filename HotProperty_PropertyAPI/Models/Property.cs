using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotProperty_PropertyAPI.Models
{
    public class Property
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Suburb { get; set; }
        public string? PostCode { get; set; }
        public string State { get; set; }
        public string ImageUrl { get; set; }
        public int AskingPrice { get; set; }    
        public int? Area { get; set; }   
        public int NoBedroom { get; set; }
        public int? NoToilet { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; }
    }
}
