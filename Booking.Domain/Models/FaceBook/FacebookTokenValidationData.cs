using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Models.FaceBook
{
    public class FacebookTokenValidationData
    {
        [JsonProperty("app_id")]
        public string AppId { get; set; } = default!;

        [JsonProperty("type")]
        public string Type { get; set; } = default!;

        [JsonProperty("application")] 
        public string Application { get; set; } = default!;

        [JsonProperty("data_access_expires_at")]
        public long DataAccessExpiresAt { get; set; } = default!;

        [JsonProperty("expires_at")]
        public long ExpiresAt { get; set; } = default!;

        [JsonProperty("is_valid")]
        public bool IsValid { get; set; } = default!;

        [JsonProperty("metadata")]
        public Metadata Metadata { get; set; } = default!;

        [JsonProperty("scopes")]
        public string[] Scopes { get; set; } = default!;

        [JsonProperty("user_id")]
        public string UserId { get; set; } = default!;
    }
}
