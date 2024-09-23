using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Entity
{
    public class RoomComfortIconType
    {
        public long Id { get; set; }
        public string ComfortName { get; set; } = null!;
        public string ComfortIcon { get; set; } = null!;
    }
}
