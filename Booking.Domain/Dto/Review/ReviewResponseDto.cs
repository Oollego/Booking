using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Dto.Review
{
    public class ReviewResponseDto
    {
        public long Id { get; set; }
        public string Comment { get; set; } = null!;
        public int FacilityScore { get; set; }
        public int StaffScore { get; set; }
        public int CleanlinessScore { get; set; }
        public int ComfortScore { get; set; }
        public int LocationScore { get; set; }
        public int ValueScore { get; set; }
        public long HotelId { get; set; }
        public string Date { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string UserSurname { get; set; } = null!;
        public string Avatar { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string HotelName { get; set;} = null!;


    }
}
