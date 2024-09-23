using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Interfaces.Services
{
    public interface IEmailService
    {
        Task SendBookEmailAsync(string email);
        Task SendConfirmationEmailAsync(string email, string confirmCode);
    }
}
