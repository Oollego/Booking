using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Dto.Hotel
{
    public class CreateHotelDto
    {
        public string HotelName { get; set; } = null!;
        public string HotelAddress { get; set; } = null!;
        public string HotelPhone { get; set; } = null!;
        public string HotelImage { get; set; } = null!;
        public string CityGuide { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Stars { get; set; }
        public int FixedDays { get; set; }
        public bool IsPet { get; set; }
        public long CityId { get; set; }
        public long HotelTypeId { get; set; }
        public long HotelChainId { get; set; }
    }
}
