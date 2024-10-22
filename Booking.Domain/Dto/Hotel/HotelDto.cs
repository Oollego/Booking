
using Booking.Domain.Dto.HotelComfort;
using Booking.Domain.Dto.NearObject;
using Booking.Domain.Resources;

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
        public decimal RoomPrice { get; set; }
        public int RoomQty { get; set; }
        public int FreeRoomQty { get; set; }
        public string CurrencyChar { get; set; } = DefaultValues.DefaultCurrancyChar;
        public List<HotelInfoLabelDto> HotelLabels { get; set; } = null!;
        public List<NearObjectDto> NearObjects { get; set; } = null!;
    }
 

}
