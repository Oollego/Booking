using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Dto.SearchFilter
{
    public class RatingFilterDto
    {
        public double Rating { get; set; }
        public int Matches { get; set; }
    }
}
