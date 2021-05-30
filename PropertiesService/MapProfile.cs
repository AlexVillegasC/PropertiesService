using AutoMapper;
using PropertyService.Dtos;
using PropertyService.Entities;

namespace PropertyService
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            // get
            CreateMap<Properties, PropertiesDto>();
        }
    }
}