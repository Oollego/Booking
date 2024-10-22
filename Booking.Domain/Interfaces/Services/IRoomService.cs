using Booking.Domain.Result;
using Booking.Domain.Dto.Room;

namespace Booking.Domain.Interfaces.Services
{
    public interface IRoomService
    {
        /// <summary>
        /// Gets all hotel rooms
        /// </summary>
        Task<CollectionResult<RoomDto>> GetRoomsAsync(long hotelId, string? email);

        /// <summary>
        /// Get room by Id
        /// </summary>
        Task<BaseResult<RoomDto>> GetRoomByIdAsync(long roomId, string? email);

        /// <summary>
        /// Create new Room
        /// </summary>
        Task<BaseResult<RoomResponseDto>> CreatRoomAsync(CreateRoomDto dto);

        /// <summary>
        /// Delete room by Id
        /// </summary>
        Task<BaseResult<RoomResponseDto>> DeleteRoomAsync(long roomId);

        /// <summary>
        /// Update room
        /// </summary>
        Task<BaseResult<RoomResponseDto>> UpdateRoomAsync(UpdateRoomDto dto);

        Task<CollectionResult<RoomsDateResponseDto>> GetRoomsForDatesByHotelId(RoomDateDto dto, string? email);
    }
}
