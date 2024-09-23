using AutoMapper;
using Booking.Domain.Dto.User;
using Booking.Domain.Entity;

namespace Booking.Application.Mapping
{
    internal class UserMapping: Profile
    {
        public UserMapping() 
        { 
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
