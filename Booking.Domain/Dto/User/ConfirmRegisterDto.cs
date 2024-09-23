using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Dto.User
{
    public record ConfirmRegisterDto(string email, string confirmCode );
   
}
