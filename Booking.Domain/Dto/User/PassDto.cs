using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Dto.User
{
    public class PassDto
    {
        public string OldPassword { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string RepeatPassword { get; set; } = null!;

    }
}
