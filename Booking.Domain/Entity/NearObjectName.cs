
namespace Booking.Domain.Entity
{
    public class NearObjectName
    {
        public long Id {  get; set; }
        public string Name { get; set; } = null!;
        public string? Icon { get; set; } = null!;
        public List<NearObject> NearObjects { get; set; } = null!;
    }
}
