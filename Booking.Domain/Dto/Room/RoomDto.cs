using Booking.Domain.Dto.Bed;
using Booking.Domain.Dto.RoomComfort;
using Booking.Domain.Dto.RoomImage;

namespace Booking.Domain.Dto.Room
{
    public class RoomDto
    {
        public long Id { get; set; }
        public string RoomName { get; set; } = null!;
        public decimal Price { get; set; }
        public decimal CancelationPrice { get; set; }
        public int Adults { get; set; }
        public int Children {  get; set; }
        public List<RoomImageDto> Images { get; set; } = null!;
        public List<BedDto> Beds { get; set; } = null!;
        public List<RoomComfortDto> RoomComforts { get; set; } = null!;
    }
}
