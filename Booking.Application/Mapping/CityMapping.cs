using AutoMapper;
using Booking.Domain.Dto.City;
using Booking.Domain.Entity;


namespace Booking.Application.Mapping
{
    internal class CityMapping: Profile
    {
        public CityMapping() 
        {
            CreateMap<City, CityDto>().ReverseMap();
        }

    }
}
