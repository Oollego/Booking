
namespace Booking.Domain.Dto.Facility
{
    public class FacilityInfoDto
    {
        public string GroupName { get; set; } = null!;
        public string GroupIcon { get; set; } = null!;
        public List<string> Facilities { get; set; } = null!;
    }
}
