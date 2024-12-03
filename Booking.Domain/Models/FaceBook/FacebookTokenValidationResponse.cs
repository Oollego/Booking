using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Models.FaceBook
{
    public class FacebookTokenValidationResponse
    {
        [JsonProperty("data")]
        public FacebookTokenValidationData Data { get; set; } = default!;
    }
}
