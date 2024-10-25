using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Dto.Facility
{
    public class UserFacilityDto
    {
        public long Id { get; set; }
        public string FacilityName { get; set; } = null!;
    }
}
