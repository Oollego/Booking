using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Settings
{
    public class FaceBookSettings
    {
        public string TokenValidationUrl { get; set; } = default!;
        public string UserInfoUrl { get; set; } = default!;
        public string AppId { get; set; } = default!;
        public string AppSecret { get; set; } = default!;
    }
}
