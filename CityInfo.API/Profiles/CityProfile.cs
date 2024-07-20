using AutoMapper;
using CityInfo.API.Entities;
using CityInfo.API.Models;

namespace CityInfo.API.Profiles
{
    public class CityProfile : Profile
    {
        public CityProfile()
        {
            CreateMap<Entities.City, Models.CityWithoutPointsOfInterestDto>();
            CreateMap<Entities.City, Models.CityDto>();
            CreateMap<PointOfInterest, PointOfInterestDto>();

            CreateMap<City, CityDto>()
           .ForMember(dest => dest.PointOfInterests, opt => opt.MapFrom(src => src.PointOfInterest));
        }
    }
}