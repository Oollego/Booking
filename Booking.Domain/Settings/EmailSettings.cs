using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Settings
{
    public class EmailSettings
    {
        public string SmtpServer { get; set; } = null!;
        public int SmtpPort { get; set; }
        public bool UseSsl { get; set; }
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
