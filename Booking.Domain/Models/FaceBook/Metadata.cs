using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Models.FaceBook
{
    public class Metadata
    {
        [JsonProperty("auth_type")]
        public string AuthType { get; set; } = default!;
    }
}
