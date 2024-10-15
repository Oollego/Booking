using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Entity
{
    public class Topic
    {
        public long Id { get; set; }
        public string TopicTitel { get; set; } = null!;
        public string TopicText { get; set; } = null!;
        public string TopicImage { get; set; } = null!;
        public List<UserProfileTopic> UserProfileTopics { get; set; } = null!;

    }
}
