
namespace Booking.Domain.Interfaces.Services.ServiceDto
{
    public class UserAuth
    {
        public string Email { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Surname { get; set; } = default!;
        public string AvatarUrl { get; set; } = default!;
    }
}
