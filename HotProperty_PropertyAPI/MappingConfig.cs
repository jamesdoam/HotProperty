using AutoMapper;
using HotProperty_PropertyAPI.Models;
using HotProperty_PropertyAPI.Models.Dto;

namespace HotProperty_PropertyAPI
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Property, PropertyDTO>();
            CreateMap<PropertyDTO, Property>();

            CreateMap<Property, PropertyCreateDTO>().ReverseMap();
            CreateMap<Property, PropertyUpdateDTO>().ReverseMap();
        }
    }
}
