using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Dto.Faq
{
    public class FaqFullDto
    {
        public long Id { get; set; }
        public string Question { get; set; } = null!;
        public string Answer { get; set; } = null!;
        public long HotelId { get; set; }
    }
}
