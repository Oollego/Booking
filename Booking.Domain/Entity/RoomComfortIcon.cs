using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Entity
{
    public class RoomComfortIcon
    {
        public long RoomId { get; set; }
        public long RoomComfortIconTypeId { get; set; }
        //public Room Room { get; set; } = null!;
        //public RoomComfortIconType RoomComfortIconType { get; set; } = null!;
    }
}
