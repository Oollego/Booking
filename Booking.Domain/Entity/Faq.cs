using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Entity
{
    public class Faq
    { 
        public long Id { get; set; }
        public string Question { get; set; } = null!;
        public string Answer { get; set; } = null!;
        public long HotelId { get; set; }
        public Hotel Hotel { get; set; } = null!;
    }
}
