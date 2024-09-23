
namespace Booking.Domain.Dto.Room
{
    public record CreateRoomDto(
       string RoomName,
       decimal RoomPrice,
       decimal CancellationPrice,
       long HotelId
     );
}
