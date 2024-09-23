using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Entity
{
    public class Facility
    {
        public long Id { get; set; }
        public string FacilityName { get; set; } = null!;
        public long FacilityGroupId { get; set; }
        public FacilityGroup FacilityGroup { get; set; } = null!;
        public List<Hotel> Hotels { get; set; } = null!;
        public List<UserProfile> UserProfiles { get; set; } = null!;
    }
}
