using AutoMapper;
using Booking.Domain.Dto.Hotel;
using Booking.Domain.Entity;

namespace Booking.Application.Mapping
{
    public class HotelMapping: Profile
    {
        public HotelMapping() 
        {
            CreateMap<CreateUpdateHotelDto, Hotel>().ReverseMap();
        }
    }
}
