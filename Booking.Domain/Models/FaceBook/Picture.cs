using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Booking.Domain.Models.FaceBook
{
    public class Picture
    {
        [JsonProperty("data")]
        public Data Data { get; set; } = default!;
    }
}
