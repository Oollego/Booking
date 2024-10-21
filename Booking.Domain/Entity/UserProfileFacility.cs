using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Entity
{
    public class UserProfileFacility
    {
        public long Id { get; set; }
        public long FacilityId { get; set; }
        public long UserProfileId { get; set; }
        public Facility Facility { get; set; } = null!;
        public UserProfile UserProfile { get; set; } = null!;
    }
}
