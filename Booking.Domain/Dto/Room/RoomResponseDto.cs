using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Dto.Room
{
    public class RoomResponseDto
    {
        public long Id { get; set; }
        public string RoomName { get; set; } = null!;
        public decimal RoomPrice {  get; set; }
        public decimal CancellationPrice { get; set; }
        public long HotelId { get; set; }
    }
}
