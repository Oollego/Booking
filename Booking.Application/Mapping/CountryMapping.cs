using AutoMapper;
using Booking.Domain.Dto.Country;
using Booking.Domain.Entity;

namespace Booking.Application.Mapping
{
    internal class CountryMapping: Profile
    {
        public CountryMapping() 
        {
            CreateMap<Country, CountryDto>().ReverseMap();
        }
    }
}
