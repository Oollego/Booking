using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Dto.Reason
{
    public class ReasonDto
    { 
        public long Id { get; set; }
        public string Reason { get; set; } = null!;
    }
}
