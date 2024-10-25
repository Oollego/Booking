using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Dto.FacilityGroup
{
    public class FacilityGroupDto
    {
        public long Id { get; set; }
        public string FacilityGroupName { get; set; } = null!;
        public string FacilityGroupIcon { get; set; } = null!;
    }
}
