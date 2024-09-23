
namespace Booking.Domain.Entity
{
    public class NearStationName
    {
        public long Id {  get; set; }
        public string Name { get; set; } = null!;
        public string? Icon { get; set; } = null!;
        public List<NearStation> NearStations { get; set; } = null!;
    }
}
