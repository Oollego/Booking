using Booking.Domain.Dto.HotelInfoCell;
using Booking.Domain.Result;

namespace Booking.Domain.Interfaces.Services
{
    public interface IInfoCellService
    {
        Task<CollectionResult<HotelInfoCellDto>> GetHotelInfoCellsAsync(long hotelId);
    }
}
