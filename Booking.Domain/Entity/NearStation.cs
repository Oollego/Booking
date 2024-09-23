
namespace Booking.Domain.Entity
{
    public class NearStation 
    {
        public long Id { get; set; }
        public int Distance { get; set; }
        public bool DistanceMetric { get; set; } = false;
        public long HotelId { get; set; }
        public Hotel Hotel { get; set; } = null!;
        public long NearPlaceNameId { get; set; }
        public NearStationName NearStationName { get; set; } = null!;
    }
}
