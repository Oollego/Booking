
namespace Booking.Application.Cashe
{
    public class RegisterUserCacheDto
    {
        public string Id { get; set; } = null!;
        public string UserEmail { get; set; } = null!;
        public string PasswordSalt { get; set; } = null!;
        public string PasswordDk { get; set; } = null!;
    }
}
