using Booking.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Entity
{
    public class HotelInfoCell
    {
        public long Id { get; set; }
        public string TextLine_1 { get; set; } = null!;
        public string TextLine_2 { get; set;} = null!;
        public long HotelId { get; set; }
        public Hotel Hotel { get; set; } = null!;
        public long? InfoIconId { get; set; }
        public InfoIcon? InfoIcon { get; set; }

    }
}
