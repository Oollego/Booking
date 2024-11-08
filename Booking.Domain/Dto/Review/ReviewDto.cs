using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Dto.Review
{
    public class ReviewDto: CreateReviewDto
    {
        public long Id { get; set; }
    }
}
