namespace Booking.Domain.Dto.Hotel
{
    public class TopHotelDto
    {
        public long HotelId { get; set; }
        public string HotelName { get; set; } = null!;
        public string HotelCity { get; set; } = null!;
        public string HotelCountry { get; set; } = null!;
        public int DistanceToCityCenter { get; set; }
        public bool DistanceMetric { get; set; }
        public int Stars { get; set; }
        public string HotelImage { get; set; } = null!;
        public int ReviewsQt { get; set; }
        public double Rating { get; set; }
        public decimal MinPrice { get; set; }
    }
}