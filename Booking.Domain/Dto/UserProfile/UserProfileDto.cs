using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Dto.UserProfile
{
    public class UserProfileDto
    {
        public string UserName { get; set; } = null!;
        public string UserSurname { get; set; } = null!;
        public string? Avatar { get; set; } = null!;
        public string? UserPhone { get; set; } = null!;
        public DateTime? DateOfBirth { get; set; }
        public bool? IsUserPet { get; set; }
        public string CurrencyCodeId { get; set; } = null!;
        public long? TravelReasonId { get; set; }
        public long? CityId { get; set; }
    }
}
