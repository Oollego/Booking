

namespace Booking.Domain.Dto.Hotel
{
    public class SearchHotelResponseDto
    {
        public int Matches { get; set; }
        public List<HotelDto> Hotels { get; set; } = null!;
        public int Count { get; set; } 
    }
}
