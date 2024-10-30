using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Dto.Faq
{
    public class FaqDto
    {
        public long FaqId { get; set; }
        public string Question { get; set; } = null!;
        public string Answer { get; set; } = null!;
    }
}
