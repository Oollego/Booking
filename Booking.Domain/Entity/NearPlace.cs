
namespace Booking.Domain.Entity
{
    public class NearPlace 
    {
        public long Id { get; set; }
        public string PlaceName { get; set; } = null!;
        public double Distance { get; set; }
        public bool DistanceMetric { get; set; } = false;
        public long HotelId { get; set; }
        public Hotel Hotel { get; set; } = null!;
        public long NearPlacesGroupId { get; set; }
        public NearPlacesGroup NearPlacesGroup { get; set; } = null!;
    }
}
