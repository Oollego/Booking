
using Booking.Domain.Dto.HotelComfort;
using Booking.Domain.Dto.NearStation;
using Booking.Domain.Entity;

namespace Booking.Domain.Dto.Hotel
{
    public class HotelDto
    {
        public long Id { get; set; }
        public string HotelName { get; set; } = null!;
        public string HotelAddress { get; set; } = null!;
        public string HotelPhone { get; set; } = null!;
        public string HotelImage { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Star { get; set; } 
        public int FixedDays { get; set; }
        public double Rating { get; set; }
        public int ReviewQty { get; set; }
        public decimal MinRoomPrice { get; set; }
        public List<HotelInfoLabelDto> HotelLabels { get; set; } = null!;
        public List<NearStationDto> NearPlaces { get; set; } = null!;
    }
 

}
