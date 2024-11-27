using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Result
{
    public class HttpMessage
    {
        public Stream StreamData { get; set; }  = default!;
        public string? ContentType { get; set; } = default!;
        public string FileName { get; set; } = default!;
    }
}
