using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Entity
{
    public class PayMethod
    {
        public long Id { get; set; }
        public string CardNumber { get; set; } = null!;
        public DateTime CardDate { get; set; }
        public long CardTypeId { get; set; }
        public CardType CardType { get; set; } = null!;
        public List<UserProfile> UserProfiles { get; set; } = null!;
    }
}
