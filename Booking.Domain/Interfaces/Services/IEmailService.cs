using Booking.Application.Services.ServiceDto;

namespace Booking.Domain.Interfaces.Services
{
    public interface IEmailService
    {
        Task SendConfirmationEmailAsync(string email, string confirmCode);
        Task SendUpdatedConfirmationEmailAsync(string email, string confirmCode);
        Task SendConfirmationBookingEmailAsync(string email, string bookingCode, ConfirmationEmailData data);
    }
}
