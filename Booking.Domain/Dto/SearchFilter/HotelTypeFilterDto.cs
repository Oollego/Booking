using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Dto.SearchFilter
{
    public class HotelTypeFilterDto
    {
        public string TypeName { get; set; } = null!;
        public int Matches { get; set; }
    }
}
