using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Entity
{
    public class HotelData
    {
        public long Id { get; set; }
        public int ReviewCount { get; set; }
        public decimal HotelMinRoomPrice { get; set; }
        public double Rating { get; set; }
        public long HotelId { get; set; }
        public Hotel? Hotel { get; set; } = null!;
    }
}
