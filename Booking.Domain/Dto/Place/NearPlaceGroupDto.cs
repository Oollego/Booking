using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Dto.Place
{
    public class NearPlaceGroupDto
    {
        public string GroupName { get; set; } = null!;
        public string GroupIcon { get; set; } = null!;
        public List<NearPlaceDto> NearPlaces { get; set; } = null!;
    }
}
