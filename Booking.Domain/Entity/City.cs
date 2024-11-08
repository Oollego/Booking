using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Entity
{
    public class City
    {
        public long Id { get; set; }
        public string CityName { get; set; } = null!;
        public long CountryId { get; set; }
        public Country Country { get; set; } = null!;
        public List<UserProfile> UserProfiles { get; set; } = null!;
        public List<Hotel> Hotels { get; set; } = null!;
        public List<OwnerProfile> OwnerProfiles { get; set; } = null!;
    }
}
