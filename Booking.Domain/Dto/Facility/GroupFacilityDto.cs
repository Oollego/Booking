using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Dto.Facility
{
    public class GroupFacilityDto
    {
        public long GroupId { get; set; }
        public string GroupName { get; set; } = null!;
        public string GroupIcon { get; set; } = null!;
        public UserFacilityDto Facility { get; set; } = null!;
    }
}
