using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Entity
{
    public class HotelChain
    {
        public long Id { get; set; }
        public string HotelChainName { get; set; } = null!;
        public List<Hotel> Hotels { get; set; } = null!;
    }
}
