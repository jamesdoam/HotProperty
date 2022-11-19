using AutoMapper;
using HotProperty_Web.Models.Dto;

namespace HotProperty_Web
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<PropertyDTO, PropertyCreateDTO>().ReverseMap();
            CreateMap<PropertyDTO, PropertyUpdateDTO>().ReverseMap();
            
            CreateMap<PropertyNumberDTO, PropertyNumberCreateDTO>().ReverseMap();
            CreateMap<PropertyNumberDTO, PropertyNumberUpdateDTO>().ReverseMap();
        }
    }
}
