namespace HotProperty_PropertyAPI.Models
{
    public class Property
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Area { get; set; }   
        public int NoBedroom { get; set; }
        public int NoToilet { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
