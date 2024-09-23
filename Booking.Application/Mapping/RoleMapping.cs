using AutoMapper;
using Booking.Domain.Dto.Role;
using Booking.Domain.Entity;

namespace Booking.Application.Mapping
{
    public class RoleMapping: Profile
    {
        public RoleMapping() 
        {
            CreateMap<Role, RoleDto>().ReverseMap();
        }
    }
}
