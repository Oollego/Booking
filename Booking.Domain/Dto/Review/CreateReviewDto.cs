﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Dto.Review
{
    public class CreateReviewDto
    {
        public string ReviewComment { get; set; } = null!;
        public int FacilityScore { get; set; }
        public int StaffScore { get; set; }
        public int CleanlinessScore { get; set; }
        public int ComfortScore { get; set; }
        public int LocationScore { get; set; }
        public int ValueScore { get; set; }
        public long HotelId { get; set; }

    }
}
