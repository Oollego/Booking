using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Entity
{
    public class Review
    {
        public long Id { get; set; }
        public string ReviewComment { get; set; } = null!;
        public DateTime ReviewDate { get; set; } = DateTime.Now;
        public int FacilityScore { get; set; }
        public int StaffScore { get; set; }
        public int CleanlinessScore { get; set; }
        public int ComfortScore { get; set; }
        public int LocationScore { get; set; }
        public int ValueScore { get; set; }
        public long HotelId { get; set; }
        public Hotel Hotel { get; set; } = null!;
        public long UserId {  get; set; }
        public User User { get; set; } = null!;

    }
}
