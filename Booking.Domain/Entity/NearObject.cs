
namespace Booking.Domain.Entity
{
    public class NearObject 
    {
        public long Id { get; set; }
        public int Distance { get; set; }
        public bool DistanceMetric { get; set; } = false;
        public long HotelId { get; set; }
        public Hotel Hotel { get; set; } = null!;
        public long NearObjectNameId { get; set; }
        public NearObjectName NearObjectName { get; set; } = null!;
    }
}
