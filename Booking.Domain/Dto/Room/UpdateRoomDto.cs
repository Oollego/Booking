
namespace Booking.Domain.Dto.Room
{ 
    public record UpdateRoomDto(
        long Id,
        string RoomName,
        decimal RoomPrice,
        decimal Cancellation
    );
}
