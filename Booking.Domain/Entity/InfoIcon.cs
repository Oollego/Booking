using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Entity
{
    public class InfoIcon
    {
        public long Id { get; set; }
        public string IconFileName { get; set; } = null!;
        public List<HotelInfoCell> HotelInfoCells { get; set; } = null!;
    }
}
