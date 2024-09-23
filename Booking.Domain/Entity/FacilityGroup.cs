using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Entity
{
    public class FacilityGroup
    {
        public long Id { get; set; }
        public string FacilityGroupName { get; set; } = null!;
        public string? FacilityGroupIcon { get; set; } = null!;
        public List<Facility> Facilities { get; set; } = null!;
    }
}
