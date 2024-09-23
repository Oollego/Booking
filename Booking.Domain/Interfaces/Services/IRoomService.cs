using Booking.Domain.Result;
using Booking.Domain.Dto.Room;

namespace Booking.Domain.Interfaces.Services
{
    public interface IRoomService
    {
        /// <summary>
        /// Gets all hotel rooms
        /// </summary>
        /// <param name="hotelId"></param>
        /// <returns></returns>
        Task<CollectionResult<RoomDto>> GetRoomsAsync(long hotelId);

        /// <summary>
        /// Get room by Id
        /// </summary>
        /// <param name="roomId"></param>
        /// <returns></returns>
        Task<BaseResult<RoomDto>> GetRoomByIdAsync(long roomId);

        /// <summary>
        /// Create new Room
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<BaseResult<RoomResponseDto>> CreatRoomAsync(CreateRoomDto dto);

        /// <summary>
        /// Delete room by Id
        /// </summary>
        /// <param name="roomId"></param>
        /// <returns></returns>
        Task<BaseResult<RoomResponseDto>> DeleteRoomAsync(long roomId);
        /// <summary>
        /// Update room
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<BaseResult<RoomResponseDto>> UpdateRoomAsync(UpdateRoomDto dto);
    }
}
