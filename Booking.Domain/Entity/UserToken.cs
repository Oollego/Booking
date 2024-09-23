using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Entity
{
    public class UserToken
    {
        public long Id { get; set; }
        public string RefreshToken { get; set; } = null!;
        public DateTime RefreshTokenExpiryTime { get; set; }
        public User User { get; set; } = null!;
        public long UserId { get; set; }
    }
}
