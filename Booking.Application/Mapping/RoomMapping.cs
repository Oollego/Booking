using AutoMapper;
using Booking.Domain.Dto.Room;
using Booking.Domain.Entity;

namespace Booking.Application.Mapping
{
    public class RoomMapping: Profile
    {
        public RoomMapping() 
        { 
            CreateMap<Room, UpdateRoomDto>().ReverseMap();
            CreateMap<Room, RoomResponseDto>().ReverseMap();
        }
    }
}
