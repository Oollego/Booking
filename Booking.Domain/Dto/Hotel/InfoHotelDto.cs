using Booking.Domain.Dto.Facility;
using Booking.Domain.Dto.HotelComfort;
using Booking.Domain.Dto.HotelInfoCell;
using Booking.Domain.Dto.Review;
using Booking.Domain.Enum;
using Booking.Domain.Resources;

namespace Booking.Domain.Dto.Hotel
{
    public class InfoHotelDto
    {
        public long Id { get; set; }
        public string HotelName { get; set; } = null!;
        public string HotelAddress { get; set; } = null!;
        public string HotelPhone { get; set; } = null!;
        public int Stars { get; set; }
        public string HotelImage { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int ReviewQty { get; set; }
        public double Rating { get; set; }
        public string CurrencyChar { get; set; } = DefaultValues.CurrancyCharUAH;
        public decimal CheapestRoom { get; set; }
        public List<HotelInfoLabelDto> HotelLabels { get; set; } = null!;
        public ScoreDto Score { get; set; } = null!;
        public List<string> Images { get; set; } = null!;
    }
}
