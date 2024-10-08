using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Dto.Topic
{
    public class TopicDto
    {
        public int Id { get; set; }
        public string TopicTitel { get; set; } = null!;
        public string TopicText { get; set; } = null!;
        public string TopicImage { get; set; } = null!;
    }
}
