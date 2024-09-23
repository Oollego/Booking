using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Dto.Review
{
    public class ScoreDto
    {
        public double FacilityScore { get; set; }
        public double StaffScore { get; set; }
        public double CleanlinessScore { get; set; }
        public double ComfortScore { get; set; }
        public double LocationScore { get; set; }
        public double ValueScore { get; set; }
    }
}
