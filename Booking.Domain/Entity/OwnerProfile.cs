using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Entity
{
    public class OwnerProfile
    {
        public long Id { get; set; }
        public string CompanyName { get; set; } = null!;
        public string? Avatar { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public long UserId { get; set; }
        public User User { get; set; } = null!;
        public long CityId { get; set; }
        public City City { get; set; } = null!;
        public string CurrencyCodeId { get; set; } = null!;
        public Currency Currency { get; set; } = null!;
        public List<OwnerPayMethod> OwnerPayMethods { get; set; } = null!;
        public List<Hotel> Hotels { get; set; } = null!;

        
    }
}
