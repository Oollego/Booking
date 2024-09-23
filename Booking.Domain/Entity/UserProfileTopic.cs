using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Entity
{
    public class UserProfileTopic
    {
        public long UserProfileId { get; set; }
        public long TopicId { get; set; }
    }
}
