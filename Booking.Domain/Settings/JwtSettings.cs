using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Settings
{
    public class JwtSettings
    {
        public const string DefaultSection = "Jwt";
        public string Issuer { get; set; } = null!;
        public string Audience { get; set; } = null!;
        public string Authority {  get; set; } = null!;
        public string JwtKey { get; set; } = null!;
        public int Lifetime { get; set; }
        public int RefreshTokenValidityInDays { get; set; }
    }
}
