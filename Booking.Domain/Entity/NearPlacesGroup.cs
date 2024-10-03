
namespace Booking.Domain.Entity
{
    public class NearPlacesGroup
    {
        public long Id {  get; set; }
        public string PlaceGroupName { get; set; } = null!;
        public string? GroupIcon { get; set; } = null!;
        public List<NearPlace> NearPlaces { get; set; } = null!;
    }
}
