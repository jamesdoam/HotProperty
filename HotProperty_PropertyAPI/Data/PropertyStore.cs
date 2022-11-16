using HotProperty_PropertyAPI.Models.Dto;

namespace HotProperty_PropertyAPI.Data
{
    public static class PropertyStore
    {
        public static List<PropertyDTO> propertyList = new List<PropertyDTO>
        {
            new PropertyDTO{Id=1,Name = "14 Davey Street",NoBedroom = 3},
            new PropertyDTO{Id=2,Name = "34 Arnold Street", NoBedroom = 4}

        };
    }
}
