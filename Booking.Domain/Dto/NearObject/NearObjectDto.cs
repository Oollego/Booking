
namespace Booking.Domain.Dto.NearObject
{
    public class NearObjectDto
    {
        public long Id { get; set; }
        public string StationName { get; set; } = null!;
        public string StationIcon { get; set; } = null!;
        public int Distance { get; set; }
        public bool DistanceMetric {  get; set; }
    }
}
