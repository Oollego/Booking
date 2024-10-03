using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Dto.Place
{
    public class NearPlaceDto
    {
        public string PlaceName { get; set; } = null!;
        public double Distance { get; set; }
        public bool DistanceMetric { get; set; }
    }
}
