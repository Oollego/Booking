using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Entity
{
    public class BedType
    {
        public long Id { get; set; }
        public string BedTypeName { get; set; } = null!;
        public int Adult { get; set; }
        public int Children { get; set; }
        public List<Room> Rooms { get; set; } = null!;
    }
}
