using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Entity
{
    public class TravelReason
    {
        public long Id {  get; set; }
        public string Reason { get; set; } = null!;
        public List<UserProfile> UserProfiles { get; set; } = null!;
    }
}
