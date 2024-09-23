using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Dto.NearStation
{
    public class NearStationDto
    {
        public long Id { get; set; }
        public string StationName { get; set; } = null!;
        public string StationIcon { get; set; } = null!;
        public int Distance { get; set; }
        public bool DistanceMetric {  get; set; }
    }
}
