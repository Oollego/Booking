
namespace Booking.Domain.Dto.Bed
{
    public class BedDto
    {
        public long Id { get; set; }
        public string BedName { get; set; } = null!;
        public int Adult { get; set; }
        public int Children { get; set; }
    }
}
