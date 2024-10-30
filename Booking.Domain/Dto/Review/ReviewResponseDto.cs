using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Dto.Review
{
    public class ReviewResponseDto: ReviewDto
    {
        public string ReviewDate { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string UserSurname { get; set; } = null!;
        public string Avatar { get; set; } = null!;
        public string Country { get; set; } = null!;

    }
}
